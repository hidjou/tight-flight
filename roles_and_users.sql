-- --------------------------------------------------------
-- Host:                         bestteamsqldbserver.database.windows.net
-- Server version:               Microsoft SQL Azure (RTM) - 12.0.2000.8
-- Server OS:                    
-- HeidiSQL Version:             9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES  */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Dumping structure for table flight_dev.AspNetRoles
DROP TABLE IF EXISTS "AspNetRoles";
CREATE TABLE IF NOT EXISTS "AspNetRoles" (
	"Id" NVARCHAR(450) NOT NULL,
	"ConcurrencyStamp" NVARCHAR(max) NULL DEFAULT NULL,
	"CreatedDate" DATETIME2(7) NOT NULL,
	"Description" NVARCHAR(max) NULL DEFAULT NULL,
	"IPAddress" NVARCHAR(max) NULL DEFAULT NULL,
	"Name" NVARCHAR(256) NULL DEFAULT NULL,
	"NormalizedName" NVARCHAR(256) NULL DEFAULT NULL,
	PRIMARY KEY ("Id")
);

-- Dumping data for table flight_dev.AspNetRoles: -1 rows
/*!40000 ALTER TABLE "AspNetRoles" DISABLE KEYS */;
REPLACE INTO "AspNetRoles" ("Id", "ConcurrencyStamp", "CreatedDate", "Description", "IPAddress", "Name", "NormalizedName") VALUES
	('557e3ec9-b099-4afe-98e9-3897c78f87d9', '6664d0a5-24a2-485d-9d91-33735ad65ba0', '2017-11-22 13:13:58.9862251', 'Somehow a boss', '::1', 'Manager', 'MANAGER'),
	('77076ad9-3579-4131-ae75-64934a171255', '4248c45e-5007-4b07-a1c6-d828058afad2', '2017-11-22 13:13:49.5605925', 'Big boss', '::1', 'Admin', 'ADMIN'),
	('b0a6b267-f3fc-474b-ae10-2d3f17b4c8c4', 'ec280fc2-7f5f-4d68-a8e3-8958babfb4bc', '2017-11-22 13:14:05.7318154', 'Not a boss', '::1', 'User', 'USER');
/*!40000 ALTER TABLE "AspNetRoles" ENABLE KEYS */;

-- Dumping structure for table flight_dev.AspNetUserRoles
DROP TABLE IF EXISTS "AspNetUserRoles";
CREATE TABLE IF NOT EXISTS "AspNetUserRoles" (
	"UserId" NVARCHAR(450) NOT NULL,
	"RoleId" NVARCHAR(450) NOT NULL,
	PRIMARY KEY ("UserId","RoleId")
);

-- Dumping data for table flight_dev.AspNetUserRoles: -1 rows
/*!40000 ALTER TABLE "AspNetUserRoles" DISABLE KEYS */;
REPLACE INTO "AspNetUserRoles" ("UserId", "RoleId") VALUES
	('de3c9928-45b6-4a57-9529-be9d3cd6ff6b', '557e3ec9-b099-4afe-98e9-3897c78f87d9'),
	('b36215ae-fca0-48a9-a173-ddf65fba96d6', '77076ad9-3579-4131-ae75-64934a171255'),
	('6f98fa8e-0795-445d-ba59-912ebfb88dd3', 'b0a6b267-f3fc-474b-ae10-2d3f17b4c8c4');
/*!40000 ALTER TABLE "AspNetUserRoles" ENABLE KEYS */;

-- Dumping structure for table flight_dev.AspNetUsers
DROP TABLE IF EXISTS "AspNetUsers";
CREATE TABLE IF NOT EXISTS "AspNetUsers" (
	"Id" NVARCHAR(450) NOT NULL,
	"AccessFailedCount" INT(10,0) NOT NULL,
	"ConcurrencyStamp" NVARCHAR(max) NULL DEFAULT NULL,
	"Email" NVARCHAR(256) NULL DEFAULT NULL,
	"EmailConfirmed" BIT NOT NULL,
	"LockoutEnabled" BIT NOT NULL,
	"LockoutEnd" DATETIMEOFFSET(7) NULL DEFAULT NULL,
	"Name" NVARCHAR(max) NULL DEFAULT NULL,
	"NormalizedEmail" NVARCHAR(256) NULL DEFAULT NULL,
	"NormalizedUserName" NVARCHAR(256) NULL DEFAULT NULL,
	"PasswordHash" NVARCHAR(max) NULL DEFAULT NULL,
	"PhoneNumber" NVARCHAR(max) NULL DEFAULT NULL,
	"PhoneNumberConfirmed" BIT NOT NULL,
	"SecurityStamp" NVARCHAR(max) NULL DEFAULT NULL,
	"TwoFactorEnabled" BIT NOT NULL,
	"UserName" NVARCHAR(256) NULL DEFAULT NULL,
	PRIMARY KEY ("Id")
);

-- Dumping data for table flight_dev.AspNetUsers: -1 rows
/*!40000 ALTER TABLE "AspNetUsers" DISABLE KEYS */;
REPLACE INTO "AspNetUsers" ("Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName") VALUES
	('6f98fa8e-0795-445d-ba59-912ebfb88dd3', 0, 'd3342045-2c53-4b17-9741-728b19746e54', NULL, 'False', 'True', NULL, NULL, NULL, 'USER', 'AQAAAAEAACcQAAAAEJS0GmRoCbxoBkAj+aiSB5ACWVtGdcs2rNicAv4croqUjyZc+AaTeoBLThiOdL70/Q==', NULL, 'False', 'c5587bd7-a8b6-4226-8421-c7139b15692e', 'False', 'user'),
	('b36215ae-fca0-48a9-a173-ddf65fba96d6', 0, '6708e2e8-d875-4fb8-a8d6-f804b7ae62ef', NULL, 'False', 'True', NULL, NULL, NULL, 'ADMIN', 'AQAAAAEAACcQAAAAECaQFWTxbsbfqsPlYsAskevHvJkkV4vhNhE4O6DXs+kMLhAAl7mJTF3REtHrjjQuFg==', NULL, 'False', '1868e959-1ff5-4726-995f-108ccf5542b0', 'False', 'admin'),
	('de3c9928-45b6-4a57-9529-be9d3cd6ff6b', 0, 'd44eaa51-6cfc-4294-ba7a-1ab3db21a492', NULL, 'False', 'True', NULL, NULL, NULL, 'MANAGER', 'AQAAAAEAACcQAAAAEKH/pAIZJ4q529g/fOxBxtxbj4+qL1F7oCs/CnylzKtr//wLhf+1q9VCu4s+U7rhVg==', NULL, 'False', 'af96d831-6319-4e5e-9ff0-89e598147edb', 'False', 'manager');
/*!40000 ALTER TABLE "AspNetUsers" ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
