using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi_nextflow.Migrations
{
    /// <inheritdoc />
    public partial class complemento_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "approval_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_approval_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "participant_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participant_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "status_flow_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    task = table.Column<bool>(type: "bit", nullable: true),
                    approval = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status_flow_items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "voting",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    task_id = table.Column<int>(type: "int", nullable: false),
                    activate_voting = table.Column<bool>(type: "bit", nullable: true),
                    start_date_vote = table.Column<DateTime>(type: "datetime2", nullable: true),
                    end_date_vote = table.Column<DateTime>(type: "datetime2", nullable: true),
                    close_voting = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voting", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "workflows",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    owner = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workflows", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "work_flow_items",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    transition_id = table.Column<int>(type: "int", nullable: false),
                    @object = table.Column<string>(name: "object", type: "nvarchar(max)", nullable: false),
                    star_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status_item_flow_id = table.Column<int>(type: "int", nullable: false),
                    evaluated_vote = table.Column<bool>(type: "bit", nullable: true),
                    voting_id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    comment = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_flow_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_work_flow_items_status_flow_items_status_item_flow_id",
                        column: x => x.status_item_flow_id,
                        principalTable: "status_flow_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_work_flow_items_voting_voting_id",
                        column: x => x.voting_id,
                        principalTable: "voting",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "groups",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    order = table.Column<int>(type: "int", nullable: false),
                    workflow_id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groups", x => x.id);
                    table.ForeignKey(
                        name: "FK_groups_workflows_workflow_id",
                        column: x => x.workflow_id,
                        principalTable: "workflows",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "specific_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order = table.Column<int>(type: "int", nullable: false),
                    workflow_id = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specific_status", x => x.id);
                    table.ForeignKey(
                        name: "FK_specific_status_workflows_workflow_id",
                        column: x => x.workflow_id,
                        principalTable: "workflows",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order = table.Column<int>(type: "int", nullable: false),
                    workflow_id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stages", x => x.id);
                    table.ForeignKey(
                        name: "FK_stages_workflows_workflow_id",
                        column: x => x.workflow_id,
                        principalTable: "workflows",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "groups_user",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groups_user", x => new { x.group_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_groups_user_groups_group_id",
                        column: x => x.group_id,
                        principalTable: "groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(700)", maxLength: 700, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    workflow_id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    stage_id = table.Column<int>(type: "int", nullable: false),
                    hastags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_items_stages_stage_id",
                        column: x => x.stage_id,
                        principalTable: "stages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_items_workflows_workflow_id",
                        column: x => x.workflow_id,
                        principalTable: "workflows",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "conditional",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    expression = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conditional", x => x.Id);
                    table.ForeignKey(
                        name: "FK_conditional_items_Id",
                        column: x => x.Id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "task",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    approval_type_id = table.Column<int>(type: "int", nullable: false),
                    time_limit = table.Column<int>(type: "int", nullable: false),
                    time_limit_type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    participant_type_id = table.Column<int>(type: "int", nullable: false),
                    group_id = table.Column<int>(type: "int", nullable: true),
                    specific_user = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    specific_status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task", x => x.id);
                    table.ForeignKey(
                        name: "FK_task_approval_type_approval_type_id",
                        column: x => x.approval_type_id,
                        principalTable: "approval_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_task_groups_group_id",
                        column: x => x.group_id,
                        principalTable: "groups",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_task_items_id",
                        column: x => x.id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_task_participant_type_participant_type_id",
                        column: x => x.participant_type_id,
                        principalTable: "participant_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_task_specific_status_specific_status_id",
                        column: x => x.specific_status_id,
                        principalTable: "specific_status",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "transition",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    current_item = table.Column<int>(type: "int", nullable: false),
                    next_item = table.Column<int>(type: "int", nullable: false),
                    event_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transition", x => x.id);
                    table.ForeignKey(
                        name: "FK_transition_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transition_items_current_item",
                        column: x => x.current_item,
                        principalTable: "items",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_transition_items_next_item",
                        column: x => x.next_item,
                        principalTable: "items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_groups_workflow_id",
                table: "groups",
                column: "workflow_id");

            migrationBuilder.CreateIndex(
                name: "IX_items_stage_id",
                table: "items",
                column: "stage_id");

            migrationBuilder.CreateIndex(
                name: "IX_items_workflow_id",
                table: "items",
                column: "workflow_id");

            migrationBuilder.CreateIndex(
                name: "IX_specific_status_workflow_id",
                table: "specific_status",
                column: "workflow_id");

            migrationBuilder.CreateIndex(
                name: "IX_stages_workflow_id",
                table: "stages",
                column: "workflow_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_approval_type_id",
                table: "task",
                column: "approval_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_group_id",
                table: "task",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_participant_type_id",
                table: "task",
                column: "participant_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_specific_status_id",
                table: "task",
                column: "specific_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_transition_current_item_next_item_event_id",
                table: "transition",
                columns: new[] { "current_item", "next_item", "event_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transition_event_id",
                table: "transition",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_transition_next_item",
                table: "transition",
                column: "next_item");

            migrationBuilder.CreateIndex(
                name: "IX_work_flow_items_status_item_flow_id",
                table: "work_flow_items",
                column: "status_item_flow_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_flow_items_voting_id",
                table: "work_flow_items",
                column: "voting_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "conditional");

            migrationBuilder.DropTable(
                name: "groups_user");

            migrationBuilder.DropTable(
                name: "task");

            migrationBuilder.DropTable(
                name: "transition");

            migrationBuilder.DropTable(
                name: "work_flow_items");

            migrationBuilder.DropTable(
                name: "approval_type");

            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropTable(
                name: "participant_type");

            migrationBuilder.DropTable(
                name: "specific_status");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "status_flow_items");

            migrationBuilder.DropTable(
                name: "voting");

            migrationBuilder.DropTable(
                name: "stages");

            migrationBuilder.DropTable(
                name: "workflows");
        }
    }
}
