CREATE TABLE `txn_testresults` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`BranchID` INT NULL DEFAULT NULL,
	`ResultDateTime` DATETIME NULL DEFAULT NULL,
	`ResultCategories` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ResultType` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`PatientID` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`PetID` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`OperatorID` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`InchargeDoctor` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`DeviceName` VARCHAR(150) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`OverallStatus` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE,
	INDEX `IX_BranchPatientID_DeviceName_Sorting` (`ID`, `BranchID`, `PatientID`, `DeviceName`, `ResultDateTime`) USING BTREE,
	INDEX `IX_BranchPatientID_DeviceName_DESCSorting` (`ID`, `BranchID`, `PatientID`, `DeviceName`, `ResultDateTime` DESC) USING BTREE,
	INDEX `IX_BranchID_DeviceName` (`ID`, `BranchID`, `DeviceName`) USING BTREE,
	INDEX `IX_BranchID_PatientID` (`ID`, `BranchID`, `PatientID`) USING BTREE,
	INDEX `IX_BranchPatientID_DeviceName` (`ID`, `BranchID`, `PatientID`, `DeviceName`) USING BTREE,
	INDEX `IX_BranchID_ResultDate` (`BranchID`, `ResultDateTime`) USING BTREE,
	INDEX `IX_BranchID_ResultDate_DESC` (`BranchID`, `ResultDateTime` DESC) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;


CREATE TABLE `txn_testresults_details` (
	`ID` BIGINT NOT NULL AUTO_INCREMENT,
	`ResultID` INT NULL DEFAULT NULL,
	`ResultParameter` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ResultSeqID` INT NULL DEFAULT NULL,
	`ResultStatus` VARCHAR(300) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ResultValue` VARCHAR(300) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ResultUnit` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ReferenceRange` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE,
	INDEX `IX_ResultID` (`ResultID`) USING BTREE,
	INDEX `IX_ResultID_ResultSeqID` (`ResultID`, `ResultSeqID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDBs
;

