CREATE TABLE `mst_account_creation_logs` (
	`ID` BIGINT NOT NULL AUTO_INCREMENT,
	`EmailAddress` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`InvitationCode` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`LinkCreatedDate` DATETIME NULL DEFAULT NULL,
	`LinkExpiryDate` DATETIME NULL DEFAULT NULL,
	`AccountCreationDate` DATETIME NULL DEFAULT NULL,
	`PatientOwnerID` INT NULL DEFAULT NULL,
	PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;
