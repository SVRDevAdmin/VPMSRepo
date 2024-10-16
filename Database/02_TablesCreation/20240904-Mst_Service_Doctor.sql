CREATE TABLE `mst_service_doctor` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`ServiceID` INT NULL DEFAULT NULL,
	`DoctorID` INT NULL DEFAULT NULL,
	`IsDeleted` INT NULL DEFAULT NULL,
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;