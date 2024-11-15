CREATE TABLE `mst_accesspermission` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`PermissionGrouping` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`PermissionKey` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`PermissionName` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`IsActive` INT NULL DEFAULT NULL,
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `mst_rolepermissions` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`RoleID` VARCHAR(255) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`PermissionKey` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`IsDeleted` INT NULL DEFAULT NULL,
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;