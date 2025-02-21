CREATE TABLE `mst_patients_login` (
	`ID` BIGINT NOT NULL AUTO_INCREMENT,
	`PatientOwnerID` BIGINT NULL DEFAULT NULL,
	`ProfileActivated` INT NULL DEFAULT NULL,
	`ActivationDate` DATETIME NULL DEFAULT NULL,
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
AUTO_INCREMENT=8
;
