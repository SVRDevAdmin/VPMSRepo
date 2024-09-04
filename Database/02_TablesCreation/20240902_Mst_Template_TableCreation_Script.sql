CREATE TABLE `mst_template` (
	`TemplateID` INT NOT NULL AUTO_INCREMENT,
	`TemplateType` VARCHAR(50) NOT NULL COLLATE 'utf8mb4_general_ci',
	`TemplateCode` VARCHAR(10) NOT NULL COLLATE 'utf8mb4_general_ci',
	`TemplateTitle` VARCHAR(200) NOT NULL COLLATE 'utf8mb4_general_ci',
	`TemplateContent` TEXT NOT NULL COLLATE 'utf8mb4_general_ci',
	`CreatedDate` DATETIME(2) NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`UpdatedDate` DATETIME(2) NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`TemplateID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;


CREATE TABLE `mst_template_details` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`TemplateID` INT NOT NULL,
	`LangCode` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`TemplateTitle` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`TemplateContent` TEXT NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;
