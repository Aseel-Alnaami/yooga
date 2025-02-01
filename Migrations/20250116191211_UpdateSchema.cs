//using System;
//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace yogago.Migrations
//{
//    /// <inheritdoc />
//    public partial class UpdateSchema : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.EnsureSchema(
//                name: "C##ASEL");

//            //migrationBuilder.CreateTable(
//            //    name: "CLASSCATEGORY",
//            //    schema: "C##ASEL",
//            //    columns: table => new
//            //    {
//            //        CATEGORYID = table.Column<decimal>(type: "NUMBER", nullable: false)
//            //            .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//            //        CATEGORYNAME = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: false)
//            //    },
//            //    constraints: table =>
//            //    {
//            //        table.PrimaryKey("SYS_C008420", x => x.CATEGORYID);
//            //    });

//            migrationBuilder.CreateTable(
//                name: "CONTACTUS",
//                schema: "C##ASEL",
//                columns: table => new
//                {
//                    CONTACTID = table.Column<decimal>(type: "NUMBER", nullable: false)
//                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                    NAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
//                    EMAIL = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
//                    SUBJECT = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
//                    MESSAGE = table.Column<string>(type: "CLOB", nullable: false),
//                    SUBMITTEDDATE = table.Column<DateTime>(type: "DATE", nullable: true, defaultValueSql: "SYSDATE\n")
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("SYS_C008491", x => x.CONTACTID);
//                });

//            migrationBuilder.CreateTable(
//                name: "DISCOUNT",
//                schema: "C##ASEL",
//                columns: table => new
//                {
//                    DISCOUNTID = table.Column<decimal>(type: "NUMBER", nullable: false)
//                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                    DISCOUNTNAME = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: false),
//                    DISCOUNTCODE = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: false),
//                    DISCOUNTPERCENTAGE = table.Column<decimal>(type: "NUMBER(5,2)", nullable: false),
//                    STARTDATE = table.Column<DateTime>(type: "DATE", nullable: true),
//                    ENDDATE = table.Column<DateTime>(type: "DATE", nullable: true),
//                    ISACTIVE = table.Column<string>(type: "CHAR(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true, defaultValueSql: "'Y' ")
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("SYS_C008484", x => x.DISCOUNTID);
//                });

//            migrationBuilder.CreateTable(
//                name: "HOMEMANAGEMENTIMG",
//                schema: "C##ASEL",
//                columns: table => new
//                {
//                    IMGID = table.Column<decimal>(type: "NUMBER", nullable: false)
//                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                    IMGNAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
//                    IMGPATH = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("SYS_C008468", x => x.IMGID);
//                });

//            migrationBuilder.CreateTable(
//                name: "HOMEMANAGEMENTTEXT",
//                schema: "C##ASEL",
//                columns: table => new
//                {
//                    TEXTID = table.Column<decimal>(type: "NUMBER", nullable: false)
//                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                    TEXTNAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
//                    CONTENT = table.Column<string>(type: "CLOB", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("SYS_C008472", x => x.TEXTID);
//                });

//            migrationBuilder.CreateTable(
//                name: "PLAN",
//                schema: "C##ASEL",
//                columns: table => new
//                {
//                    PLANID = table.Column<decimal>(type: "NUMBER", nullable: false)
//                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                    PLANNAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
//                    DURATIONMONTHS = table.Column<decimal>(type: "NUMBER", nullable: false),
//                    PRICE = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("SYS_C008433", x => x.PLANID);
//                });

//            migrationBuilder.CreateTable(
//                name: "ROLES",
//                schema: "C##ASEL",
//                columns: table => new
//                {
//                    ROLEID = table.Column<decimal>(type: "NUMBER", nullable: false),
//                    ROLENAME = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("SYS_C008396", x => x.ROLEID);
//                });

//            migrationBuilder.CreateTable(
//                name: "USERINFO",
//                schema: "C##ASEL",
//                columns: table => new
//                {
//                    USERID = table.Column<decimal>(type: "NUMBER", nullable: false)
//                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                    FULLNAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
//                    USERNAME = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: false),
//                    EMAIL = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
//                    PHONE = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: true),
//                    DATEOFBIRTH = table.Column<DateTime>(type: "DATE", nullable: true),
//                    ADDRESS = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
//                    PROFILEPICTURE = table.Column<string>(type: "VARCHAR2(200)", unicode: false, maxLength: 200, nullable: true),
//                    PASSWORD = table.Column<string>(type: "VARCHAR2(200)", unicode: false, maxLength: 200, nullable: false),
//                    ROLEID = table.Column<decimal>(type: "NUMBER", nullable: false),
//                    DATEADDED = table.Column<DateTime>(type: "DATE", nullable: true, defaultValueSql: "SYSDATE\n")
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("SYS_C008405", x => x.USERID);
//                    table.ForeignKey(
//                        name: "SYS_C008407",
//                        column: x => x.ROLEID,
//                        principalSchema: "C##ASEL",
//                        principalTable: "ROLES",
//                        principalColumn: "ROLEID");
//                });

//            migrationBuilder.CreateTable(
//                name: "MEMBER",
//                schema: "C##ASEL",
//                columns: table => new
//                {
//                    MEMBERID = table.Column<decimal>(type: "NUMBER", nullable: false)
//                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                    USERID = table.Column<decimal>(type: "NUMBER", nullable: true),
//                    JOINDATE = table.Column<DateTime>(type: "DATE", nullable: true, defaultValueSql: "SYSDATE  -- Date the user became a member\n")
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("SYS_C008435", x => x.MEMBERID);
//                    table.ForeignKey(
//                        name: "SYS_C008436",
//                        column: x => x.USERID,
//                        principalSchema: "C##ASEL",
//                        principalTable: "USERINFO",
//                        principalColumn: "USERID",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "TRAINER",
//                schema: "C##ASEL",
//                columns: table => new
//                {
//                    TRAINERID = table.Column<decimal>(type: "NUMBER", nullable: false)
//                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                    USERID = table.Column<decimal>(type: "NUMBER", nullable: true),
//                    SPECIALIZATION = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: true),
//                    EXPERIENCE = table.Column<decimal>(type: "NUMBER", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("SYS_C008409", x => x.TRAINERID);
//                    table.ForeignKey(
//                        name: "SYS_C008410",
//                        column: x => x.USERID,
//                        principalSchema: "C##ASEL",
//                        principalTable: "USERINFO",
//                        principalColumn: "USERID",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "USERLOGIN",
//                schema: "C##ASEL",
//                columns: table => new
//                {
//                    LOGINID = table.Column<decimal>(type: "NUMBER", nullable: false)
//                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                    USERID = table.Column<decimal>(type: "NUMBER", nullable: true),
//                    USERNAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
//                    PASSWORD = table.Column<string>(type: "VARCHAR2(200)", unicode: false, maxLength: 200, nullable: false),
//                    ROLEID = table.Column<decimal>(type: "NUMBER", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("SYS_C008414", x => x.LOGINID);
//                    table.ForeignKey(
//                        name: "SYS_C008416",
//                        column: x => x.USERID,
//                        principalSchema: "C##ASEL",
//                        principalTable: "USERINFO",
//                        principalColumn: "USERID",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "SYS_C008417",
//                        column: x => x.ROLEID,
//                        principalSchema: "C##ASEL",
//                        principalTable: "ROLES",
//                        principalColumn: "ROLEID",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "MEMBERPLAN",
//                schema: "C##ASEL",
//                columns: table => new
//                {
//                    MEMBERPLANID = table.Column<decimal>(type: "NUMBER", nullable: false)
//                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                    MEMBERID = table.Column<decimal>(type: "NUMBER", nullable: true),
//                    PLANID = table.Column<decimal>(type: "NUMBER", nullable: true),
//                    STARTDATE = table.Column<DateTime>(type: "DATE", nullable: true, defaultValueSql: "SYSDATE\n")
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("SYS_C008438", x => x.MEMBERPLANID);
//                    table.ForeignKey(
//                        name: "SYS_C008439",
//                        column: x => x.MEMBERID,
//                        principalSchema: "C##ASEL",
//                        principalTable: "MEMBER",
//                        principalColumn: "MEMBERID",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "SYS_C008440",
//                        column: x => x.PLANID,
//                        principalSchema: "C##ASEL",
//                        principalTable: "PLAN",
//                        principalColumn: "PLANID",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "PAYMENTINFO",
//                schema: "C##ASEL",
//                columns: table => new
//                {
//                    CARDID = table.Column<decimal>(type: "NUMBER", nullable: false)
//                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                    MEMBERID = table.Column<decimal>(type: "NUMBER", nullable: true),
//                    FULLNAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
//                    EMAIL = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
//                    ZIPCODE = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: false),
//                    CARDNUMBERENCRYPTED = table.Column<string>(type: "CLOB", nullable: false),
//                    CVVENCRYPTED = table.Column<string>(type: "VARCHAR2(200)", unicode: false, maxLength: 200, nullable: false),
//                    EXPIRYMONTH = table.Column<byte>(type: "NUMBER(2)", precision: 2, nullable: false),
//                    EXPIRYYEAR = table.Column<byte>(type: "NUMBER(4)", precision: 4, nullable: false),
//                    CREATEDDATE = table.Column<DateTime>(type: "DATE", nullable: true, defaultValueSql: "SYSDATE -- Timestamp when the record was created\n")
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("SYS_C008453", x => x.CARDID);
//                    table.ForeignKey(
//                        name: "SYS_C008454",
//                        column: x => x.MEMBERID,
//                        principalSchema: "C##ASEL",
//                        principalTable: "MEMBER",
//                        principalColumn: "MEMBERID",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "TESTIMONIAL",
//                schema: "C##ASEL",
//                columns: table => new
//                {
//                    TESTIMONIALID = table.Column<decimal>(type: "NUMBER", nullable: false)
//                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                    MEMBERID = table.Column<decimal>(type: "NUMBER", nullable: true),
//                    CONTENT = table.Column<string>(type: "CLOB", nullable: false),
//                    RATING = table.Column<decimal>(type: "NUMBER", nullable: true),
//                    STATUS = table.Column<string>(type: "VARCHAR2(10)", unicode: false, maxLength: 10, nullable: true, defaultValueSql: "'Pending' "),
//                    SUBMITTEDDATE = table.Column<DateTime>(type: "DATE", nullable: true, defaultValueSql: "SYSDATE                             -- Timestamp of submission\n")
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("SYS_C008477", x => x.TESTIMONIALID);
//                    table.ForeignKey(
//                        name: "SYS_C008478",
//                        column: x => x.MEMBERID,
//                        principalSchema: "C##ASEL",
//                        principalTable: "MEMBER",
//                        principalColumn: "MEMBERID",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "CLASSES",
//                schema: "C##ASEL",
//                columns: table => new
//                {
//                    CLASSID = table.Column<decimal>(type: "NUMBER", nullable: false)
//                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                    CLASSNAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
//                    CLASSDAYS = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: false),
//                    CLASSTIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: false),
//                    IMGCOVER = table.Column<string>(type: "VARCHAR2(200)", unicode: false, maxLength: 200, nullable: true),
//                    CATEGORYID = table.Column<decimal>(type: "NUMBER", nullable: true),
//                    TRAINERID = table.Column<decimal>(type: "NUMBER", nullable: true),
//                    PRICE = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("SYS_C008426", x => x.CLASSID);
//                    table.ForeignKey(
//                        name: "SYS_C008427",
//                        column: x => x.CATEGORYID,
//                        principalSchema: "C##ASEL",
//                        principalTable: "CLASSCATEGORY",
//                        principalColumn: "CATEGORYID",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "SYS_C008428",
//                        column: x => x.TRAINERID,
//                        principalSchema: "C##ASEL",
//                        principalTable: "TRAINER",
//                        principalColumn: "TRAINERID",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "INVOICEINFO",
//                schema: "C##ASEL",
//                columns: table => new
//                {
//                    INVOICEID = table.Column<decimal>(type: "NUMBER", nullable: false)
//                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                    MEMBERPLANID = table.Column<decimal>(type: "NUMBER", nullable: true),
//                    AMOUNT = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false),
//                    GENERATEDDATE = table.Column<DateTime>(type: "DATE", nullable: true, defaultValueSql: "SYSDATE"),
//                    EMAILSENT = table.Column<string>(type: "CHAR(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true, defaultValueSql: "'N' ")
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("SYS_C008458", x => x.INVOICEID);
//                    table.ForeignKey(
//                        name: "SYS_C008459",
//                        column: x => x.MEMBERPLANID,
//                        principalSchema: "C##ASEL",
//                        principalTable: "MEMBERPLAN",
//                        principalColumn: "MEMBERPLANID",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "CART",
//                schema: "C##ASEL",
//                columns: table => new
//                {
//                    CARTID = table.Column<decimal>(type: "NUMBER", nullable: false)
//                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                    MEMBERID = table.Column<decimal>(type: "NUMBER", nullable: false),
//                    CLASSID = table.Column<decimal>(type: "NUMBER", nullable: false),
//                    ADDEDDATE = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "SYSDATE")
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_CART", x => x.CARTID);
//                    table.ForeignKey(
//                        name: "FK_CART_CLASSES",
//                        column: x => x.CLASSID,
//                        principalSchema: "C##ASEL",
//                        principalTable: "CLASSES",
//                        principalColumn: "CLASSID",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "FK_CART_MEMBER",
//                        column: x => x.MEMBERID,
//                        principalSchema: "C##ASEL",
//                        principalTable: "MEMBER",
//                        principalColumn: "MEMBERID",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "CLASSMEMBER",
//                schema: "C##ASEL",
//                columns: table => new
//                {
//                    CLASSMEMBERID = table.Column<decimal>(type: "NUMBER", nullable: false)
//                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                    CLASSID = table.Column<decimal>(type: "NUMBER", nullable: true),
//                    MEMBERID = table.Column<decimal>(type: "NUMBER", nullable: true),
//                    PaymentStatus = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
//                    EnrollmentDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("SYS_C008442", x => x.CLASSMEMBERID);
//                    table.ForeignKey(
//                        name: "SYS_C008443",
//                        column: x => x.CLASSID,
//                        principalSchema: "C##ASEL",
//                        principalTable: "CLASSES",
//                        principalColumn: "CLASSID",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "SYS_C008444",
//                        column: x => x.MEMBERID,
//                        principalSchema: "C##ASEL",
//                        principalTable: "MEMBER",
//                        principalColumn: "MEMBERID",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateIndex(
//                name: "IX_CART_CLASSID",
//                schema: "C##ASEL",
//                table: "CART",
//                column: "CLASSID");

//            migrationBuilder.CreateIndex(
//                name: "IX_CART_MEMBERID",
//                schema: "C##ASEL",
//                table: "CART",
//                column: "MEMBERID");

//            migrationBuilder.CreateIndex(
//                name: "IX_CLASSES_CATEGORYID",
//                schema: "C##ASEL",
//                table: "CLASSES",
//                column: "CATEGORYID");

//            migrationBuilder.CreateIndex(
//                name: "IX_CLASSES_TRAINERID",
//                schema: "C##ASEL",
//                table: "CLASSES",
//                column: "TRAINERID");

//            migrationBuilder.CreateIndex(
//                name: "IX_CLASSMEMBER_CLASSID",
//                schema: "C##ASEL",
//                table: "CLASSMEMBER",
//                column: "CLASSID");

//            migrationBuilder.CreateIndex(
//                name: "IX_CLASSMEMBER_MEMBERID",
//                schema: "C##ASEL",
//                table: "CLASSMEMBER",
//                column: "MEMBERID");

//            migrationBuilder.CreateIndex(
//                name: "SYS_C008485",
//                schema: "C##ASEL",
//                table: "DISCOUNT",
//                column: "DISCOUNTCODE",
//                unique: true);

//            migrationBuilder.CreateIndex(
//                name: "IX_INVOICEINFO_MEMBERPLANID",
//                schema: "C##ASEL",
//                table: "INVOICEINFO",
//                column: "MEMBERPLANID");

//            migrationBuilder.CreateIndex(
//                name: "IX_MEMBER_USERID",
//                schema: "C##ASEL",
//                table: "MEMBER",
//                column: "USERID");

//            migrationBuilder.CreateIndex(
//                name: "IX_MEMBERPLAN_MEMBERID",
//                schema: "C##ASEL",
//                table: "MEMBERPLAN",
//                column: "MEMBERID");

//            migrationBuilder.CreateIndex(
//                name: "IX_MEMBERPLAN_PLANID",
//                schema: "C##ASEL",
//                table: "MEMBERPLAN",
//                column: "PLANID");

//            migrationBuilder.CreateIndex(
//                name: "IX_PAYMENTINFO_MEMBERID",
//                schema: "C##ASEL",
//                table: "PAYMENTINFO",
//                column: "MEMBERID");

//            migrationBuilder.CreateIndex(
//                name: "SYS_C008397",
//                schema: "C##ASEL",
//                table: "ROLES",
//                column: "ROLENAME",
//                unique: true);

//            migrationBuilder.CreateIndex(
//                name: "IX_TESTIMONIAL_MEMBERID",
//                schema: "C##ASEL",
//                table: "TESTIMONIAL",
//                column: "MEMBERID");

//            migrationBuilder.CreateIndex(
//                name: "IX_TRAINER_USERID",
//                schema: "C##ASEL",
//                table: "TRAINER",
//                column: "USERID");

//            migrationBuilder.CreateIndex(
//                name: "IX_USERINFO_ROLEID",
//                schema: "C##ASEL",
//                table: "USERINFO",
//                column: "ROLEID");

//            migrationBuilder.CreateIndex(
//                name: "SYS_C008406",
//                schema: "C##ASEL",
//                table: "USERINFO",
//                column: "USERNAME",
//                unique: true);

//            migrationBuilder.CreateIndex(
//                name: "IX_USERLOGIN_ROLEID",
//                schema: "C##ASEL",
//                table: "USERLOGIN",
//                column: "ROLEID");

//            migrationBuilder.CreateIndex(
//                name: "IX_USERLOGIN_USERID",
//                schema: "C##ASEL",
//                table: "USERLOGIN",
//                column: "USERID");

//            migrationBuilder.CreateIndex(
//                name: "SYS_C008415",
//                schema: "C##ASEL",
//                table: "USERLOGIN",
//                column: "USERNAME",
//                unique: true);
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "CART",
//                schema: "C##ASEL");

//            migrationBuilder.DropTable(
//                name: "CLASSMEMBER",
//                schema: "C##ASEL");

//            migrationBuilder.DropTable(
//                name: "CONTACTUS",
//                schema: "C##ASEL");

//            migrationBuilder.DropTable(
//                name: "DISCOUNT",
//                schema: "C##ASEL");

//            migrationBuilder.DropTable(
//                name: "HOMEMANAGEMENTIMG",
//                schema: "C##ASEL");

//            migrationBuilder.DropTable(
//                name: "HOMEMANAGEMENTTEXT",
//                schema: "C##ASEL");

//            migrationBuilder.DropTable(
//                name: "INVOICEINFO",
//                schema: "C##ASEL");

//            migrationBuilder.DropTable(
//                name: "PAYMENTINFO",
//                schema: "C##ASEL");

//            migrationBuilder.DropTable(
//                name: "TESTIMONIAL",
//                schema: "C##ASEL");

//            migrationBuilder.DropTable(
//                name: "USERLOGIN",
//                schema: "C##ASEL");

//            migrationBuilder.DropTable(
//                name: "CLASSES",
//                schema: "C##ASEL");

//            migrationBuilder.DropTable(
//                name: "MEMBERPLAN",
//                schema: "C##ASEL");

//            migrationBuilder.DropTable(
//                name: "CLASSCATEGORY",
//                schema: "C##ASEL");

//            migrationBuilder.DropTable(
//                name: "TRAINER",
//                schema: "C##ASEL");

//            migrationBuilder.DropTable(
//                name: "MEMBER",
//                schema: "C##ASEL");

//            migrationBuilder.DropTable(
//                name: "PLAN",
//                schema: "C##ASEL");

//            migrationBuilder.DropTable(
//                name: "USERINFO",
//                schema: "C##ASEL");

//            migrationBuilder.DropTable(
//                name: "ROLES",
//                schema: "C##ASEL");
//        }
//    }
//}


using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace yogago.Migrations
{
    public partial class UpdateSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "C##ASEL");

            // Skip creating CLASSCATEGORY as it already exists.
            // migrationBuilder.CreateTable(
            //     name: "CLASSCATEGORY",
            //     schema: "C##ASEL",
            //     columns: table => new
            //     {
            //         CATEGORYID = table.Column<decimal>(type: "NUMBER", nullable: false)
            //             .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
            //         CATEGORYNAME = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("SYS_C008420", x => x.CATEGORYID);
            //     });

            migrationBuilder.CreateTable(
                name: "CONTACTUS",
                schema: "C##ASEL",
                columns: table => new
                {
                    CONTACTID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    SUBJECT = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    MESSAGE = table.Column<string>(type: "CLOB", nullable: false),
                    SUBMITTEDDATE = table.Column<DateTime>(type: "DATE", nullable: true, defaultValueSql: "SYSDATE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008491", x => x.CONTACTID);
                });

            migrationBuilder.CreateTable(
                name: "DISCOUNT",
                schema: "C##ASEL",
                columns: table => new
                {
                    DISCOUNTID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DISCOUNTNAME = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: false),
                    DISCOUNTCODE = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: false),
                    DISCOUNTPERCENTAGE = table.Column<decimal>(type: "NUMBER(5,2)", nullable: false),
                    STARTDATE = table.Column<DateTime>(type: "DATE", nullable: true),
                    ENDDATE = table.Column<DateTime>(type: "DATE", nullable: true),
                    ISACTIVE = table.Column<string>(type: "CHAR(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true, defaultValueSql: "'Y'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008484", x => x.DISCOUNTID);
                });

            migrationBuilder.CreateTable(
                name: "HOMEMANAGEMENTIMG",
                schema: "C##ASEL",
                columns: table => new
                {
                    IMGID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IMGNAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    IMGPATH = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008468", x => x.IMGID);
                });

            migrationBuilder.CreateTable(
                name: "HOMEMANAGEMENTTEXT",
                schema: "C##ASEL",
                columns: table => new
                {
                    TEXTID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TEXTNAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    CONTENT = table.Column<string>(type: "CLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008472", x => x.TEXTID);
                });

            migrationBuilder.CreateTable(
                name: "PLAN",
                schema: "C##ASEL",
                columns: table => new
                {
                    PLANID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PLANNAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    DURATIONMONTHS = table.Column<decimal>(type: "NUMBER", nullable: false),
                    PRICE = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008433", x => x.PLANID);
                });

            migrationBuilder.CreateTable(
                name: "CLASSES",
                schema: "C##ASEL",
                columns: table => new
                {
                    CLASSID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CLASSNAME = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    CLASSDAYS = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: false),
                    CLASSTIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: false),
                    IMGCOVER = table.Column<string>(type: "VARCHAR2(200)", unicode: false, maxLength: 200, nullable: true),
                    CATEGORYID = table.Column<decimal>(type: "NUMBER", nullable: true),
                    TRAINERID = table.Column<decimal>(type: "NUMBER", nullable: true),
                    PRICE = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008426", x => x.CLASSID);
                    table.ForeignKey(
                        name: "SYS_C008427",
                        column: x => x.CATEGORYID,
                        principalSchema: "C##ASEL",
                        principalTable: "CLASSCATEGORY",
                        principalColumn: "CATEGORYID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "SYS_C008428",
                        column: x => x.TRAINERID,
                        principalSchema: "C##ASEL",
                        principalTable: "TRAINER",
                        principalColumn: "TRAINERID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CART",
                schema: "C##ASEL",
                columns: table => new
                {
                    CARTID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    MEMBERID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    CLASSID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    ADDEDDATE = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "SYSDATE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CART", x => x.CARTID);
                    table.ForeignKey(
                        name: "FK_CART_CLASSES",
                        column: x => x.CLASSID,
                        principalSchema: "C##ASEL",
                        principalTable: "CLASSES",
                        principalColumn: "CLASSID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CART_MEMBER",
                        column: x => x.MEMBERID,
                        principalSchema: "C##ASEL",
                        principalTable: "MEMBER",
                        principalColumn: "MEMBERID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Retain the existing Down method logic to clean up added tables if rolled back.
            migrationBuilder.DropTable(name: "CART", schema: "C##ASEL");
            migrationBuilder.DropTable(name: "CLASSES", schema: "C##ASEL");
            migrationBuilder.DropTable(name: "CONTACTUS", schema: "C##ASEL");
        }
    }
}
