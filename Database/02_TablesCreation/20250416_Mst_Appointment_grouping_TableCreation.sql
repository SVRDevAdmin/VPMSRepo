CREATE TABLE `mst_appointment_grouping` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`AppointmentGroup` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`AppointmentSubGroup` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`AppointmentSubGrpValue` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`SeqNo` INT NULL DEFAULT NULL,
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE,
	INDEX `IX_ApptGrp_SeqNo` (`AppointmentGroup`, `SeqNo`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;