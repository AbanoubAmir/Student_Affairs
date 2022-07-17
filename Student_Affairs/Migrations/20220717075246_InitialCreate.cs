using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Student_Affairs.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Classes",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Classes", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Subjects",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Subjects", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Students",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(nullable: false),
            //        Address = table.Column<string>(maxLength: 80, nullable: false),
            //        DateOfBirth = table.Column<DateTime>(nullable: false),
            //        Email = table.Column<string>(nullable: false),
            //        ClassID = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Students", x => x.ID);
            //        table.ForeignKey(
            //            name: "FK_Students_Classes_ClassID",
            //            column: x => x.ClassID,
            //            principalTable: "Classes",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "StudentSubjects",
            //    columns: table => new
            //    {
            //        StudentID = table.Column<int>(nullable: false),
            //        SubjectID = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_StudentSubjects", x => new { x.StudentID, x.SubjectID });
            //        table.ForeignKey(
            //            name: "FK_StudentSubjects_Students_StudentID",
            //            column: x => x.StudentID,
            //            principalTable: "Students",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_StudentSubjects_Subjects_SubjectID",
            //            column: x => x.SubjectID,
            //            principalTable: "Subjects",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Students_ClassID",
            //    table: "Students",
            //    column: "ClassID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_StudentSubjects_SubjectID",
            //    table: "StudentSubjects",
            //    column: "SubjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentSubjects");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Classes");
        }
    }
}
