CREATE TABLE `mst_mastercodedata` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`CodeGroup` VARCHAR(20) NOT NULL COLLATE 'utf8mb4_general_ci',
	`CodeID` VARCHAR(20) NOT NULL COLLATE 'utf8mb4_general_ci',
	`CodeName` VARCHAR(20) NOT NULL COLLATE 'utf8mb4_general_ci',
	`Description` VARCHAR(100) NOT NULL COLLATE 'utf8mb4_general_ci',
	`IsActive` BIT(1) NOT NULL,
	`SeqOrder` INT NOT NULL,
	`CreatedDate` DATETIME(2) NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`UpdatedDate` DATETIME(2) NULL DEFAULT NULL,
	`updatedBy` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;