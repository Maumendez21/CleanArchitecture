using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Data.Migrations
{
    public partial class migrationsRelaciones2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Director_Videos_VideoId",
                table: "Director");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoActor_Actor_ActorId",
                table: "VideoActor");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoActor_Videos_VideoId",
                table: "VideoActor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoActor",
                table: "VideoActor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Director",
                table: "Director");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actor",
                table: "Actor");

            migrationBuilder.RenameTable(
                name: "VideoActor",
                newName: "VideoActors");

            migrationBuilder.RenameTable(
                name: "Director",
                newName: "Directors");

            migrationBuilder.RenameTable(
                name: "Actor",
                newName: "Actors");

            migrationBuilder.RenameIndex(
                name: "IX_VideoActor_VideoId",
                table: "VideoActors",
                newName: "IX_VideoActors_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_Director_VideoId",
                table: "Directors",
                newName: "IX_Directors_VideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoActors",
                table: "VideoActors",
                columns: new[] { "ActorId", "VideoId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Directors",
                table: "Directors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actors",
                table: "Actors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Directors_Videos_VideoId",
                table: "Directors",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoActors_Actors_ActorId",
                table: "VideoActors",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoActors_Videos_VideoId",
                table: "VideoActors",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Directors_Videos_VideoId",
                table: "Directors");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoActors_Actors_ActorId",
                table: "VideoActors");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoActors_Videos_VideoId",
                table: "VideoActors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoActors",
                table: "VideoActors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Directors",
                table: "Directors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actors",
                table: "Actors");

            migrationBuilder.RenameTable(
                name: "VideoActors",
                newName: "VideoActor");

            migrationBuilder.RenameTable(
                name: "Directors",
                newName: "Director");

            migrationBuilder.RenameTable(
                name: "Actors",
                newName: "Actor");

            migrationBuilder.RenameIndex(
                name: "IX_VideoActors_VideoId",
                table: "VideoActor",
                newName: "IX_VideoActor_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_Directors_VideoId",
                table: "Director",
                newName: "IX_Director_VideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoActor",
                table: "VideoActor",
                columns: new[] { "ActorId", "VideoId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Director",
                table: "Director",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actor",
                table: "Actor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Director_Videos_VideoId",
                table: "Director",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoActor_Actor_ActorId",
                table: "VideoActor",
                column: "ActorId",
                principalTable: "Actor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoActor_Videos_VideoId",
                table: "VideoActor",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
