using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Affairs.Data;
using Student_Affairs.Models;
using Student_Affairs.Models.Helpers;
using Student_Affairs.Models.StudentAffairViewModels;

namespace Student_Affairs.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentAffairsContext _context;

        public StudentsController(StudentAffairsContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string sortOrder,string currentFilter,string searchString,int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewData["DateOfBirthSortParm"] = sortOrder == "DateOfBirth" ? "DateOfBirth_desc" : "DateOfBirth";
            ViewData["ClassSortParm"] = sortOrder == "ClassID" ? "ClassID_desc" : "ClassID";
            ViewData["AddressSortParm"] = sortOrder == "Address" ? "Address_desc" : "Address";
            ViewData["EmailSortParm"] = sortOrder == "Email" ? "Email_desc" : "Email";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var students = from s in _context.Students
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString));
            }
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "Name";
            }
            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder[..^5];
                descending = true;
            }

            if (descending)
            {
                students = students.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                students = students.OrderBy(e => EF.Property<object>(e, sortOrder));
            }
            int pageSize = 10;
            return View(await PaginatedList<Student>.CreateAsync(students.AsNoTracking().Include(s=>s.Class),
                pageNumber ?? 1, pageSize));
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s=>s.Class)
                .Include(s => s.StudentSubjects)
                .ThenInclude(s => s.Subject)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            var student = new Student
            {
                StudentSubjects = new List<StudentSubject>()
            };
            PopulateAssignedSubjectData(student);
            PopulateClassesDropDownList();
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,DateOfBirth,Email,ClassID")] Student student, string[] selectedSubjects)
        {
            if (selectedSubjects != null)
            {
                student.StudentSubjects = new List<StudentSubject>();
                foreach (var subject in selectedSubjects)
                {
                    var subjectToAdd = new StudentSubject { StudentID = student.ID, SubjectID = int.Parse(subject) };
                    student.StudentSubjects.Add(subjectToAdd);
                }
            }
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(ex.Message, "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            PopulateAssignedSubjectData(student);
            PopulateClassesDropDownList(student.ClassID);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(s => s.Class)
                .Include(s => s.StudentSubjects)
                .ThenInclude(s => s.Subject)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.ID == id);
            if (student == null)
            {
                return NotFound();
            }
            PopulateClassesDropDownList(student.ClassID);
            PopulateAssignedSubjectData(student);
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedSubjects)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Students
                .Include(s => s.Class)
                .Include(s => s.StudentSubjects)
                .ThenInclude(s => s.Subject)
                .FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Student>(
                studentToUpdate,
                "",
                s => s.Name, s => s.Address, s => s.DateOfBirth, s => s.Email, s => s.ClassID))
            {
                UpdateStudentSubjects(selectedSubjects, studentToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { id = id });
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError(ex.Message, "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            PopulateClassesDropDownList(studentToUpdate.ClassID);
            UpdateStudentSubjects(selectedSubjects, studentToUpdate);
            PopulateAssignedSubjectData(studentToUpdate);
            return View(studentToUpdate);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false, string errorMessage = "")
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Class)
                .Include(s => s.StudentSubjects)
                .ThenInclude(s => s.Subject)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator. \n"
                    +errorMessage;
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.Include(s => s.StudentSubjects).SingleAsync(s => s.ID == id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true, errorMessage = ex.Message });
            }
        }

        private void PopulateClassesDropDownList(object selectedClass = null)
        {
            var classQuery = from s in _context.Classes
                                   orderby s.Name
                                   select s;
            ViewBag.ClassID = new SelectList(classQuery.AsNoTracking(), "ID", "Name", selectedClass);
        }

        private void PopulateAssignedSubjectData(Student student)
        {
            var allSubjects = _context.Subjects;
            var studentSubjects = new HashSet<int>(student.StudentSubjects.Select(c => c.SubjectID));
            var viewModel = new List<AssignedSubjectData>();
            foreach (var subject in allSubjects)
            {
                viewModel.Add(new AssignedSubjectData
                {
                    SubjectID = subject.ID,
                    SubjectName = subject.Name,
                    Assigned = studentSubjects.Contains(subject.ID)
                });
            }
            ViewData["Subjects"] = viewModel;
        }

        private void UpdateStudentSubjects(string[] selectedSubjects, Student studentToUpdate)
        {
            if (selectedSubjects == null)
            {
                studentToUpdate.StudentSubjects = new List<StudentSubject>();
                return;
            }

            var selectedSubjectsHS = new HashSet<string>(selectedSubjects);
            var studentSubjects = new HashSet<int>
                (studentToUpdate.StudentSubjects.Select(c => c.Subject.ID));
            foreach (var subject in _context.Subjects)
            {
                if (selectedSubjectsHS.Contains(subject.ID.ToString()))
                {
                    if (!studentSubjects.Contains(subject.ID))
                    {
                        studentToUpdate.StudentSubjects.Add(new StudentSubject { StudentID = studentToUpdate.ID, SubjectID = subject.ID });
                    }
                }
                else
                {

                    if (studentSubjects.Contains(subject.ID))
                    {
                        StudentSubject subjectToRemove = studentToUpdate.StudentSubjects.FirstOrDefault(i => i.SubjectID == subject.ID);
                        _context.Remove(subjectToRemove);
                    }
                }
            }
        }
    }
}
