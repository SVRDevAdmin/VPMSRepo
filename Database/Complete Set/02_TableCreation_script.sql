-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               8.3.0 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Version:             12.10.0.7000
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

-- Dumping structure for table vpmsdb.aspnetroleclaims
CREATE TABLE IF NOT EXISTS `aspnetroleclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.aspnetroles
CREATE TABLE IF NOT EXISTS `aspnetroles` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.aspnetuserclaims
CREATE TABLE IF NOT EXISTS `aspnetuserclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.aspnetuserlogins
CREATE TABLE IF NOT EXISTS `aspnetuserlogins` (
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderKey` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.aspnetuserroles
CREATE TABLE IF NOT EXISTS `aspnetuserroles` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.aspnetusers
CREATE TABLE IF NOT EXISTS `aspnetusers` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.aspnetusertokens
CREATE TABLE IF NOT EXISTS `aspnetusertokens` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_accesspermission
CREATE TABLE IF NOT EXISTS `mst_accesspermission` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `PermissionGrouping` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `PermissionKey` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `PermissionName` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `IsActive` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_IsActive` (`IsActive`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_account_creation_logs
CREATE TABLE IF NOT EXISTS `mst_account_creation_logs` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `EmailAddress` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `InvitationCode` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `LinkCreatedDate` datetime DEFAULT NULL,
  `LinkExpiryDate` datetime DEFAULT NULL,
  `AccountCreationDate` datetime DEFAULT NULL,
  `PatientOwnerID` int DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_appointment
CREATE TABLE IF NOT EXISTS `mst_appointment` (
  `AppointmentID` bigint NOT NULL AUTO_INCREMENT,
  `UniqueIDKey` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `BranchID` int DEFAULT NULL,
  `ApptDate` date DEFAULT NULL,
  `ApptStartTime` time DEFAULT NULL,
  `ApptEndTime` time DEFAULT NULL,
  `OwnerID` bigint DEFAULT NULL,
  `PetID` bigint DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `EmailNotify` bit(1) DEFAULT NULL,
  `InchargeDoctor` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`AppointmentID`),
  KEY `IX_ValidationDocAppt` (`BranchID`,`Status`,`InchargeDoctor`,`ApptDate`,`ApptStartTime`,`ApptEndTime`),
  KEY `IX_ValidationOwnerAppt` (`BranchID`,`Status`,`OwnerID`,`PetID`,`ApptDate`,`ApptStartTime`,`ApptEndTime`),
  KEY `IX_AppointmentView` (`PetID`,`OwnerID`,`ApptDate`,`Status`,`InchargeDoctor`),
  KEY `IX_AppointmentView_Sort` (`ApptStartTime`),
  KEY `IX_API_ApptByCreatedDate` (`AppointmentID`,`PetID`,`OwnerID`,`CreatedDate`,`UpdatedDate`),
  KEY `IX_API_ApptByUpdatedDate` (`AppointmentID`,`PetID`,`OwnerID`,`UpdatedDate`),
  KEY `IX_API_ApptByUniqueID` (`AppointmentID`,`PetID`,`OwnerID`,`UniqueIDKey`),
  KEY `IX_OwnerID` (`OwnerID`),
  KEY `IX_ApptID_BranchID` (`AppointmentID`,`BranchID`),
  KEY `IX_CustView_ValidateAppt` (`AppointmentID`,`Status`,`BranchID`,`ApptDate`,`ApptStartTime`,`ApptEndTime`),
  KEY `IX_CustView_ValidateApptByDoc` (`Status`,`BranchID`,`InchargeDoctor`,`ApptDate`,`ApptStartTime`,`ApptEndTime`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_appointment_grouping
CREATE TABLE IF NOT EXISTS `mst_appointment_grouping` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `AppointmentGroup` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `AppointmentSubGroup` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `AppointmentSubGrpValue` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `SeqNo` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_ApptGrp_SeqNo` (`AppointmentGroup`,`SeqNo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_appointment_services
CREATE TABLE IF NOT EXISTS `mst_appointment_services` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `ApptID` bigint DEFAULT NULL,
  `ServicesID` bigint DEFAULT NULL,
  `IsDeleted` bit(1) NOT NULL DEFAULT (0),
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_AppointmentView_Services` (`ServicesID`,`IsDeleted`),
  KEY `IX_ApptID_ServicesID_IsDeleted` (`ApptID`,`ServicesID`,`IsDeleted`),
  KEY `IX_ApptID_IsDeleted` (`ApptID`,`IsDeleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_avatar
CREATE TABLE IF NOT EXISTS `mst_avatar` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Species` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `AvatarImage` varchar(150) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ColorCode` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_banners
CREATE TABLE IF NOT EXISTS `mst_banners` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `BannerType` int DEFAULT NULL,
  `Description` varchar(300) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `SeqOrder` int DEFAULT NULL,
  `IsActive` int DEFAULT NULL,
  `StartDate` datetime DEFAULT NULL,
  `EndDate` datetime DEFAULT NULL,
  `BannerName` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `BannerFilePath` varchar(500) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_blogs
CREATE TABLE IF NOT EXISTS `mst_blogs` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(150) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Description` varchar(300) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `URLtoRedirect` varchar(1000) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `SeqOrder` int DEFAULT NULL,
  `IsActive` int DEFAULT NULL,
  `StartDate` datetime DEFAULT NULL,
  `EndDate` datetime DEFAULT NULL,
  `ThumbnailImage` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ThumbnailFilePath` varchar(500) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_branch
CREATE TABLE IF NOT EXISTS `mst_branch` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `OrganizationID` int NOT NULL,
  `Name` varchar(200) NOT NULL,
  `ContactNo` varchar(20) NOT NULL,
  `Address` varchar(500) NOT NULL,
  `Status` int NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `CreatedBy` varchar(50) NOT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `Postcode` varchar(20) DEFAULT NULL,
  `City` varchar(100) DEFAULT NULL,
  `State` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_OrganizationID` (`OrganizationID`),
  KEY `IX_OrganizationID_Status` (`OrganizationID`,`Status`),
  KEY `IX_BranchID_Status` (`ID`,`Status`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_city
CREATE TABLE IF NOT EXISTS `mst_city` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `StateID` int NOT NULL,
  `City` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedDate` datetime(2) DEFAULT NULL,
  `CreatedBy` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime(2) DEFAULT NULL,
  `UpdatedBy` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_client
CREATE TABLE IF NOT EXISTS `mst_client` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Description` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `BranchID` int DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_client_auth
CREATE TABLE IF NOT EXISTS `mst_client_auth` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ClientID` int DEFAULT NULL,
  `ClientKey` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `StartDate` date DEFAULT NULL,
  `EndDate` date DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_ClientKey` (`ClientKey`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_countrylist
CREATE TABLE IF NOT EXISTS `mst_countrylist` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `CountryCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CountryName` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  KEY `IX_CountryName_IsActive` (`CountryName`,`IsActive`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_currency
CREATE TABLE IF NOT EXISTS `mst_currency` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Country` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CurrencyCode` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CurrencySymbol` varchar(5) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `DisplayFormat` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_doctor
CREATE TABLE IF NOT EXISTS `mst_doctor` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Gender` varchar(1) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `LicenseNo` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Designation` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Specialty` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `System_ID` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `IsDeleted` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedDateTimestamp` timestamp NULL DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedDateTimestamp` timestamp NULL DEFAULT NULL,
  `UpdatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `BranchID` int DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  KEY `IX_ViewList` (`Name`,`Gender`,`BranchID`,`IsDeleted`),
  KEY `IX_ViewList_WithSorting` (`Name`,`Gender`,`BranchID`,`IsDeleted`,`ID`),
  KEY `IX_ViewList_SortOrder` (`ID`,`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_doctor_services
CREATE TABLE IF NOT EXISTS `mst_doctor_services` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `DoctorID` int DEFAULT NULL,
  `ServicesID` int DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_invoicereceipt
CREATE TABLE IF NOT EXISTS `mst_invoicereceipt` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `TreatmentPlanID` int DEFAULT NULL,
  `Branch` int DEFAULT NULL,
  `InvoiceNo` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ReceiptNo` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `PetName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `OwnerName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Fee` decimal(18,2) DEFAULT NULL,
  `Tax` decimal(18,2) DEFAULT NULL,
  `GrandDiscount` decimal(18,2) DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_locationlist
CREATE TABLE IF NOT EXISTS `mst_locationlist` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `System_LocationID` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `System_LocationName` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `System_Status` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_mastercodedata
CREATE TABLE IF NOT EXISTS `mst_mastercodedata` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `CodeGroup` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CodeID` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CodeName` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Description` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `IsActive` bit(1) NOT NULL,
  `SeqOrder` int NOT NULL,
  `CreatedDate` datetime(2) DEFAULT NULL,
  `CreatedBy` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime(2) DEFAULT NULL,
  `updatedBy` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  KEY `IX_CodeGroup_CodeID` (`CodeGroup`,`CodeID`),
  KEY `IX_CodeGroup_CodeID_IsActive` (`CodeGroup`,`CodeID`,`IsActive`),
  KEY `IX_CodeGroup` (`CodeGroup`),
  KEY `IX_CodeGroup_IsActive_SeqOrder` (`CodeGroup`,`IsActive`,`SeqOrder`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_medicalrecord_medication
CREATE TABLE IF NOT EXISTS `mst_medicalrecord_medication` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `PetID` int DEFAULT NULL,
  `CategoryID` int DEFAULT NULL,
  `ProductID` int DEFAULT NULL,
  `ExpiryDate` datetime DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `Description` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_medicalrecord_vaccinationsurgery
CREATE TABLE IF NOT EXISTS `mst_medicalrecord_vaccinationsurgery` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `PetID` int DEFAULT NULL,
  `CategoryID` int DEFAULT NULL,
  `ServiceID` int DEFAULT NULL,
  `Type` int DEFAULT NULL,
  `DueDate` datetime DEFAULT NULL,
  `Description` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Remarks` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `IsDeleted` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_notification_receiver_config
CREATE TABLE IF NOT EXISTS `mst_notification_receiver_config` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `NotificationGroup` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `RoleID` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `NotificationSend` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_NotifGroup_RoleID` (`NotificationGroup`,`RoleID`),
  KEY `IX_NotifGroup` (`NotificationGroup`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_organisation
CREATE TABLE IF NOT EXISTS `mst_organisation` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Level` int DEFAULT NULL,
  `ParentID` int DEFAULT NULL,
  `Name` varchar(200) NOT NULL,
  `TotalStaff` int NOT NULL,
  `Status` int NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `CreatedBy` varchar(50) NOT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_ID_Level` (`ID`,`Level`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_patients
CREATE TABLE IF NOT EXISTS `mst_patients` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `BranchID` int NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `CreatedBy` varchar(50) NOT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_patients_configuration
CREATE TABLE IF NOT EXISTS `mst_patients_configuration` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `UserID` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ConfigurationKey` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ConfigurationValue` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_patients_login
CREATE TABLE IF NOT EXISTS `mst_patients_login` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `PatientOwnerID` bigint DEFAULT NULL,
  `ProfileActivated` int DEFAULT NULL,
  `ActivationDate` datetime DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `AspnetUserID` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_patients_owner
CREATE TABLE IF NOT EXISTS `mst_patients_owner` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `PatientID` bigint NOT NULL,
  `Name` varchar(200) NOT NULL,
  `Gender` varchar(2) NOT NULL,
  `ContactNo` varchar(15) NOT NULL,
  `EmailAddress` varchar(50) NOT NULL,
  `Address` varchar(200) NOT NULL,
  `PostCode` varchar(20) NOT NULL,
  `City` varchar(100) NOT NULL,
  `State` varchar(100) NOT NULL,
  `Country` varchar(100) NOT NULL,
  `Status` int NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `CreatedBy` varchar(50) NOT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `IsPrimary` int DEFAULT NULL,
  `ProfileAvatarID` int DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_Gender_PatientID` (`Gender`,`PatientID`),
  KEY `IX_Gender` (`Gender`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_pets
CREATE TABLE IF NOT EXISTS `mst_pets` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `PatientID` bigint NOT NULL,
  `Name` varchar(100) NOT NULL,
  `RegistrationNo` varchar(50) NOT NULL,
  `Gender` varchar(2) NOT NULL,
  `DOB` date NOT NULL,
  `Age` int NOT NULL,
  `Species` varchar(10) NOT NULL,
  `Breed` varchar(100) NOT NULL,
  `Color` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Allergies` varchar(150) NOT NULL,
  `Weight` decimal(6,2) NOT NULL,
  `WeightUnit` varchar(5) NOT NULL,
  `Height` decimal(6,2) NOT NULL,
  `HeightUnit` varchar(5) NOT NULL,
  `Status` int NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `CreatedBy` varchar(50) NOT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `AvatarID` int DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_PatientID_AvatarID_Gender` (`PatientID`,`AvatarID`,`Gender`),
  KEY `IX_PatientID` (`PatientID`),
  KEY `IX_Gender` (`Gender`),
  KEY `IX_Gender_AvatarID` (`Gender`,`AvatarID`),
  KEY `IX_Gender_PatientID` (`Gender`,`PatientID`),
  KEY `IX_Listing_Gender_PatientID_Name_Species_Breed` (`Gender`,`PatientID`,`Name`,`Species`,`Breed`),
  KEY `IX_Listing_Gender_PatientID_Name_Species` (`Gender`,`PatientID`,`Name`,`Species`),
  KEY `IX_Listing_Gender_PatientID_Name` (`Gender`,`PatientID`,`Name`),
  KEY `IX_Listing_Gender_PatientID_Species_Breed` (`Gender`,`PatientID`,`Species`,`Breed`),
  KEY `IX_Listing_Gender_PatientID_Species` (`Gender`,`PatientID`,`Species`),
  KEY `IX_Listing_Gender_PatientID_Breed` (`Gender`,`PatientID`,`Breed`),
  KEY `IX_Listing_Gender_PatientID_Name_Breed` (`Gender`,`PatientID`,`Name`,`Breed`),
  KEY `IX_Gender_AvatarID_PetID` (`Gender`,`AvatarID`,`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_pets_breed
CREATE TABLE IF NOT EXISTS `mst_pets_breed` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `Species` varchar(50) NOT NULL,
  `Breed` varchar(100) NOT NULL,
  `Active` int NOT NULL,
  `SeqOrder` int NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `CreatedBy` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_Species_Active_SeqOrder` (`Species`,`Active`,`SeqOrder`),
  KEY `IX_Species_Active` (`Species`,`Active`),
  KEY `IX_SeqOrder` (`SeqOrder`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_pets_ranges
CREATE TABLE IF NOT EXISTS `mst_pets_ranges` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Species` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `RangeType` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `RangeStart` decimal(20,4) DEFAULT NULL,
  `RangeEnd` decimal(20,4) DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  KEY `ID` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_pet_growth
CREATE TABLE IF NOT EXISTS `mst_pet_growth` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `PetID` int DEFAULT NULL,
  `Age` int DEFAULT NULL,
  `Height` decimal(6,2) NOT NULL,
  `Weight` decimal(6,2) NOT NULL,
  `Allergies` varchar(150) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `BMI` decimal(6,2) NOT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_product
CREATE TABLE IF NOT EXISTS `mst_product` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ProductTypeID` int DEFAULT NULL,
  `BranchID` int DEFAULT NULL,
  `SKU` varchar(30) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `InventoryName` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Name` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `RecommendedWeight` decimal(6,2) DEFAULT NULL,
  `PricePerQty` decimal(10,2) DEFAULT NULL,
  `Species` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `RecommendedBreed` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Usage` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Description` text COLLATE utf8mb4_general_ci,
  `ImageFilePath` text COLLATE utf8mb4_general_ci,
  `ImageFileName` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_producttype
CREATE TABLE IF NOT EXISTS `mst_producttype` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `TypeName` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_product_status
CREATE TABLE IF NOT EXISTS `mst_product_status` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ProductID` int DEFAULT NULL,
  `StockStatus` int DEFAULT NULL,
  `QtyInStores` int DEFAULT NULL,
  `LowStockThreshold` int DEFAULT NULL,
  `ExpiryDate` datetime DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_profile_avatar
CREATE TABLE IF NOT EXISTS `mst_profile_avatar` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `EntityGroup` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `EntitySubGroup` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `AvatarFileName` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `AvatarFilePath` varchar(500) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_rolepermissions
CREATE TABLE IF NOT EXISTS `mst_rolepermissions` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `RoleID` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `PermissionKey` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `IsDeleted` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_RoleID_IsDeleted` (`RoleID`,`IsDeleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_roles
CREATE TABLE IF NOT EXISTS `mst_roles` (
  `RoleID` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `RoleName` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `RoleType` int NOT NULL,
  `BranchID` int DEFAULT NULL,
  `Status` int NOT NULL,
  `IsAdmin` int DEFAULT NULL,
  `IsDoctor` int DEFAULT NULL,
  `Description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CreatedDate` datetime NOT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `OrganizationID` int DEFAULT NULL,
  PRIMARY KEY (`RoleID`),
  KEY `IX_RoleID_Status` (`RoleID`,`Status`),
  KEY `IX_RoleID_RoleName` (`RoleID`,`RoleName`),
  KEY `IX_RoleID_Branch_Status` (`RoleID`,`BranchID`,`Status`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_services
CREATE TABLE IF NOT EXISTS `mst_services` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `CategoryID` int DEFAULT NULL,
  `Name` varchar(150) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Prices` decimal(18,2) DEFAULT NULL,
  `Duration` decimal(6,2) DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `Description` text COLLATE utf8mb4_general_ci,
  `Precaution` text COLLATE utf8mb4_general_ci,
  `BranchID` int DEFAULT NULL,
  `DoctorInCharge` varchar(150) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_servicescategory
CREATE TABLE IF NOT EXISTS `mst_servicescategory` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `SubCategoryName` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_service_doctor
CREATE TABLE IF NOT EXISTS `mst_service_doctor` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ServiceID` int DEFAULT NULL,
  `DoctorID` int DEFAULT NULL,
  `IsDeleted` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_state
CREATE TABLE IF NOT EXISTS `mst_state` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `CountryID` int NOT NULL,
  `State` varchar(20) COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedDate` datetime(2) DEFAULT NULL,
  `CreatedBy` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime(2) DEFAULT NULL,
  `UpdatedBy` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_CountryID_State` (`CountryID`,`State`),
  KEY `IX_State` (`State`),
  KEY `IX_CountryID` (`CountryID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_template
CREATE TABLE IF NOT EXISTS `mst_template` (
  `TemplateID` int NOT NULL AUTO_INCREMENT,
  `TemplateType` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TemplateCode` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TemplateTitle` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TemplateContent` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedDate` datetime(2) DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime(2) DEFAULT NULL,
  `UpdatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`TemplateID`) USING BTREE,
  KEY `IX_TemplateIDCode` (`TemplateID`,`TemplateCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_template_details
CREATE TABLE IF NOT EXISTS `mst_template_details` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `TemplateID` int NOT NULL,
  `LangCode` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `TemplateTitle` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `TemplateContent` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  KEY `IX_TemplateIDLanguageCode` (`TemplateID`,`LangCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_testslist
CREATE TABLE IF NOT EXISTS `mst_testslist` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `System_TestID` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `System_TestName` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `System_Description` varchar(150) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `IsActive` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_timezone
CREATE TABLE IF NOT EXISTS `mst_timezone` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `TimezoneID` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Location` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `IsActive` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_treatmentplan
CREATE TABLE IF NOT EXISTS `mst_treatmentplan` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `BranchID` int DEFAULT NULL,
  `Remarks` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci,
  `TotalPrice` decimal(18,2) DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `IsDeleted` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_treatmentplan_products
CREATE TABLE IF NOT EXISTS `mst_treatmentplan_products` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `PlanID` int DEFAULT NULL,
  `ProductID` int DEFAULT NULL,
  `ProductName` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Units` int DEFAULT NULL,
  `PricePerQty` decimal(10,2) DEFAULT NULL,
  `TotalPrice` decimal(10,2) DEFAULT NULL,
  `IsDeleted` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_treatmentplan_services
CREATE TABLE IF NOT EXISTS `mst_treatmentplan_services` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `PlanID` int DEFAULT NULL,
  `ServiceID` int DEFAULT NULL,
  `ServiceName` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Price` decimal(18,2) DEFAULT NULL,
  `IsDeleted` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_user
CREATE TABLE IF NOT EXISTS `mst_user` (
  `UserID` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `Surname` varchar(200) COLLATE utf8mb4_general_ci NOT NULL,
  `LastName` varchar(200) COLLATE utf8mb4_general_ci NOT NULL,
  `StaffID` varchar(200) COLLATE utf8mb4_general_ci NOT NULL,
  `Gender` varchar(2) COLLATE utf8mb4_general_ci NOT NULL,
  `EmailAddress` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `Status` int NOT NULL,
  `RoleID` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `BranchID` int NOT NULL,
  `Level1ID` int NOT NULL,
  `LastLoginDate` datetime DEFAULT NULL,
  `CreatedDate` datetime NOT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `OrganizationID` int DEFAULT NULL,
  PRIMARY KEY (`UserID`),
  KEY `IX_UserViewListing` (`UserID`,`RoleID`,`BranchID`,`Status`,`Gender`),
  KEY `IX_Surname_Ordering` (`Surname`),
  KEY `IX_UserViewListing_Sorting` (`UserID`,`RoleID`,`BranchID`,`Status`,`Gender`,`Surname`),
  KEY `IX_UserID_BranchID` (`UserID`,`BranchID`),
  KEY `IX_Status_OrgID_BranchID` (`Status`,`OrganizationID`,`BranchID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.mst_users_configuration
CREATE TABLE IF NOT EXISTS `mst_users_configuration` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `UserID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ConfigurationKey` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ConfigurationValue` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `idx_UserID_ConfigKey` (`UserID`,`ConfigurationKey`),
  KEY `IX_UserID` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.patients_configuration_logs
CREATE TABLE IF NOT EXISTS `patients_configuration_logs` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `UserID` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `TransactionKey` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `TransactionValue` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `TransactionDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_customer_expenses_summary
CREATE TABLE IF NOT EXISTS `txn_customer_expenses_summary` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `TransDate` date DEFAULT NULL,
  `TransDateInMonth` varchar(2) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `TransDateInYear` varchar(5) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `PatientID` bigint DEFAULT NULL,
  `PetID` bigint DEFAULT NULL,
  `ServiceID` int DEFAULT NULL,
  `ServiceName` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `TotalValue` decimal(10,6) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` datetime DEFAULT NULL,
  `EntityType` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_customer_expenses_summarylog
CREATE TABLE IF NOT EXISTS `txn_customer_expenses_summarylog` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `TransactionType` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `TransactionDate` date DEFAULT NULL,
  `ExecutionType` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_customer_loginsession
CREATE TABLE IF NOT EXISTS `txn_customer_loginsession` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `SessionID` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `SessionCreatedOn` datetime DEFAULT NULL,
  `SessionExpiredOn` datetime DEFAULT NULL,
  `UserID` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `LoginID` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_customer_loginsession_log
CREATE TABLE IF NOT EXISTS `txn_customer_loginsession_log` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `ActionType` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `SessionID` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `SessionCreatedOn` datetime DEFAULT NULL,
  `SessionExpiredOn` datetime DEFAULT NULL,
  `LoginID` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_customer_notifications
CREATE TABLE IF NOT EXISTS `txn_customer_notifications` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `NotificationGroup` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `NotificationType` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Title` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Content` text COLLATE utf8mb4_general_ci,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_customer_notification_receiver
CREATE TABLE IF NOT EXISTS `txn_customer_notification_receiver` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `NotificationID` bigint DEFAULT NULL,
  `UserID` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `MsgReadDateTime` datetime DEFAULT NULL,
  `MsgDeletedDateTime` datetime DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_invoicereceiptno
CREATE TABLE IF NOT EXISTS `txn_invoicereceiptno` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `OrganisationAbbr` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `BranchID` int DEFAULT NULL,
  `Year` varchar(4) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Month` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Day` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `RunningNo` int DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_loginsession
CREATE TABLE IF NOT EXISTS `txn_loginsession` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `SessionID` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SessionCreatedOn` datetime NOT NULL,
  `SessionExpiredOn` datetime NOT NULL,
  `UserID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_SessionExpiredOn` (`SessionExpiredOn`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_loginsession_log
CREATE TABLE IF NOT EXISTS `txn_loginsession_log` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ActionType` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SessionID` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SessionCreatedOn` datetime NOT NULL,
  `SessionExpiredOn` datetime NOT NULL,
  `UserID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_notifications
CREATE TABLE IF NOT EXISTS `txn_notifications` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `BranchID` int DEFAULT NULL,
  `NotificationGroup` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `NotificationType` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Title` varchar(200) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Content` text COLLATE utf8mb4_general_ci,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_NotiGroup` (`NotificationGroup`),
  KEY `IX_NotiGroup_CreatedDate` (`NotificationGroup`,`CreatedDate` DESC),
  KEY `IX_Notification_OrderSorting` (`CreatedDate` DESC)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_notification_receiver
CREATE TABLE IF NOT EXISTS `txn_notification_receiver` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `NotificationID` bigint DEFAULT NULL,
  `TargetUser` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `MsgReadDateTime` datetime DEFAULT NULL,
  `MsgDeletedDateTime` datetime DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_NotiID_UserID_Status` (`NotificationID`,`TargetUser`,`Status`),
  KEY `IX_NotiID_UserID` (`NotificationID`,`TargetUser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_scheduledtests_submission
CREATE TABLE IF NOT EXISTS `txn_scheduledtests_submission` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `PatientID` bigint DEFAULT NULL,
  `ScheduledDate` datetime DEFAULT NULL,
  `TestID` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `TestName` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `LocationID` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `LocationName` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `SubmissionSent` datetime DEFAULT NULL,
  `ResponseStatus` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_stock_transaction
CREATE TABLE IF NOT EXISTS `txn_stock_transaction` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `ProductID` int DEFAULT NULL,
  `SerialNo` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ExpiryDate` date DEFAULT NULL,
  `StockInDateTime` datetime DEFAULT NULL,
  `StockInBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `StockOutDateTime` datetime DEFAULT NULL,
  `StockOutBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_stock_upload
CREATE TABLE IF NOT EXISTS `txn_stock_upload` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `UploadLogID` int DEFAULT NULL,
  `SKU` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `PricePerQty` decimal(10,2) DEFAULT NULL,
  `SerialNo` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_stock_uploadlog
CREATE TABLE IF NOT EXISTS `txn_stock_uploadlog` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ActionType` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Filename` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `BranchID` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_testresults
CREATE TABLE IF NOT EXISTS `txn_testresults` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `BranchID` int DEFAULT NULL,
  `ResultDateTime` datetime DEFAULT NULL,
  `ResultCategories` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ResultType` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `PatientID` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `PetID` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `OperatorID` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `InchargeDoctor` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `DeviceName` varchar(150) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `OverallStatus` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_BranchPatientID_DeviceName_Sorting` (`ID`,`BranchID`,`PatientID`,`DeviceName`,`ResultDateTime`),
  KEY `IX_BranchPatientID_DeviceName_DESCSorting` (`ID`,`BranchID`,`PatientID`,`DeviceName`,`ResultDateTime` DESC),
  KEY `IX_BranchID_DeviceName` (`ID`,`BranchID`,`DeviceName`),
  KEY `IX_BranchID_PatientID` (`ID`,`BranchID`,`PatientID`),
  KEY `IX_BranchPatientID_DeviceName` (`ID`,`BranchID`,`PatientID`,`DeviceName`),
  KEY `IX_BranchID_ResultDate` (`BranchID`,`ResultDateTime`),
  KEY `IX_BranchID_ResultDate_DESC` (`BranchID`,`ResultDateTime` DESC),
  KEY `IX_DeviceName_Grouping` (`DeviceName`),
  KEY `IX_BranchID_DeviceName_Grouping` (`BranchID`,`DeviceName`),
  KEY `IX_PetID_ResultType_ResultDateTime` (`PetID`,`ResultType`,`ResultDateTime`),
  KEY `IX_PetID_ResultDateTime` (`PetID`,`ResultDateTime`),
  KEY `IX_ResultDateTime` (`ResultDateTime`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_testresults_details
CREATE TABLE IF NOT EXISTS `txn_testresults_details` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `ResultID` int DEFAULT NULL,
  `ResultParameter` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ResultSeqID` int DEFAULT NULL,
  `ResultStatus` varchar(300) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ResultValue` varchar(300) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ResultUnit` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ReferenceRange` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_ResultID` (`ResultID`),
  KEY `IX_ResultID_ResultSeqID` (`ResultID`,`ResultSeqID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_transactionsummary
CREATE TABLE IF NOT EXISTS `txn_transactionsummary` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `SummaryType` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `SummaryDate` date DEFAULT NULL,
  `BranchID` int DEFAULT NULL,
  `DateInYear` varchar(4) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `DateInMonth` varchar(2) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Week` int DEFAULT NULL,
  `Quarter` int DEFAULT NULL,
  `Group` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `SubGroup` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `TotalAmount` decimal(14,2) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_transactionsummarylog
CREATE TABLE IF NOT EXISTS `txn_transactionsummarylog` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `TransactionType` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `TransactionDate` date DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_treatmentplan
CREATE TABLE IF NOT EXISTS `txn_treatmentplan` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `PetID` int DEFAULT NULL,
  `TreatmentPlanID` int DEFAULT NULL,
  `PlanName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `TreatmentStart` datetime DEFAULT NULL,
  `TreatmentEnd` datetime DEFAULT NULL,
  `TotalCost` decimal(10,2) DEFAULT NULL,
  `Remarks` text COLLATE utf8mb4_general_ci,
  `Status` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  KEY `IX_PetID_CreatedDate` (`ID`,`PetID`,`CreatedDate` DESC),
  KEY `IX_PetID_TreatmentDate` (`PetID`,`TreatmentStart`,`TreatmentEnd`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_treatmentplan_products
CREATE TABLE IF NOT EXISTS `txn_treatmentplan_products` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `PlanID` int DEFAULT NULL,
  `ProductID` int DEFAULT NULL,
  `ProductName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Units` int DEFAULT NULL,
  `PricePerQty` decimal(10,2) DEFAULT NULL,
  `Discount` decimal(10,2) DEFAULT NULL,
  `TotalPrice` decimal(10,2) DEFAULT NULL,
  `IsDeleted` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  KEY `IX_PlanId_ProductID` (`PlanID`,`ProductID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

-- Dumping structure for table vpmsdb.txn_treatmentplan_services
CREATE TABLE IF NOT EXISTS `txn_treatmentplan_services` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `PlanID` int DEFAULT NULL,
  `ServiceID` int DEFAULT NULL,
  `ServiceName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Price` decimal(10,2) DEFAULT NULL,
  `Discount` decimal(10,2) DEFAULT NULL,
  `TotalPrice` decimal(10,2) DEFAULT NULL,
  `IsDeleted` int DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  KEY `IX_PlanID_ServiceID` (`PlanID`,`ServiceID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Data exporting was unselected.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
