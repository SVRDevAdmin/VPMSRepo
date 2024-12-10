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
	PRIMARY KEY (`ID`) USING BTREE
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
	PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

