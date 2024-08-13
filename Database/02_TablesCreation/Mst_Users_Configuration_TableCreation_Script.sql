CREATE TABLE `mst_users_configuration` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`UserID` VARCHAR(30) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ConfigurationKey` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ConfigurationValue` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE,
	INDEX `idx_UserID_ConfigKey` (`UserID`, `ConfigurationKey`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
AUTO_INCREMENT=3
;
