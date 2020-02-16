using Microsoft.EntityFrameworkCore.Migrations;

namespace Sequence.Data.Migrations
{
    public partial class AddedBatchIdToProcessesSequence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BatchId",
                table: "ProcessedSequences",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "ProcessedSequences");
        }
    }
}
