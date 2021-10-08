using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Migrations
{
    public partial class ChangeAttendanceClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_StudentsCourses_StudentCourseCourseId_StudentCourseStudentId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_StudentCourseCourseId_StudentCourseStudentId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "StudentCourseCourseId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "StudentCourseId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "StudentCourseStudentId",
                table: "Attendances");

            migrationBuilder.AddColumn<long>(
                name: "StudentId",
                table: "Attendances",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Students_StudentId",
                table: "Attendances",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Students_StudentId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Attendances");

            migrationBuilder.AddColumn<long>(
                name: "StudentCourseCourseId",
                table: "Attendances",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StudentCourseId",
                table: "Attendances",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StudentCourseStudentId",
                table: "Attendances",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StudentCourseCourseId_StudentCourseStudentId",
                table: "Attendances",
                columns: new[] { "StudentCourseCourseId", "StudentCourseStudentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_StudentsCourses_StudentCourseCourseId_StudentCourseStudentId",
                table: "Attendances",
                columns: new[] { "StudentCourseCourseId", "StudentCourseStudentId" },
                principalTable: "StudentsCourses",
                principalColumns: new[] { "CourseId", "StudentId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
