CREATE TABLE `txn_testresults` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`ResultDateTime` DATETIME NULL DEFAULT NULL,
	`ResultType` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ResultStatus` VARCHAR(300) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ResultValue` VARCHAR(300) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ResultParameter` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ReferenceRange` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`PatientID` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`PetID` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`OperatorID` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`InchargeDoctor` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`PetName` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;
