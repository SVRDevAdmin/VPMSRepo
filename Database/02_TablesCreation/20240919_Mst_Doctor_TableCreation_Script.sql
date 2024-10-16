CREATE TABLE `mst_doctor` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`Name` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Gender` VARCHAR(1) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`LicenseNo` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Designation` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Specialty` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`System_ID` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`IsDeleted` INT NULL DEFAULT NULL,
	`BranchID` INT NULL DEFAULT NULL,
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE,
	INDEX `IX_ViewList` (`Name`, `Gender`, `IsDeleted`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;
