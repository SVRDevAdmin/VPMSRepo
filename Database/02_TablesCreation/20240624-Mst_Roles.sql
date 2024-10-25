CREATE TABLE `mst_roles` (
	`RoleID` VARCHAR(255) NOT NULL COLLATE 'utf8mb4_general_ci',
	`RoleName` VARCHAR(100) NOT NULL COLLATE 'utf8mb4_general_ci',
	`RoleType` INT NOT NULL,
	`Status` INT NOT NULL,
	`IsAdmin` INT NULL DEFAULT NULL,
	`IsDoctor` INT NULL DEFAULT NULL,
	`CreatedDate` DATETIME NOT NULL,
	`CreatedBy` VARCHAR(50) NOT NULL COLLATE 'utf8mb4_general_ci',
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`BranchID` INT NULL DEFAULT NULL,
	`Description` VARCHAR(255) NULL DEFAULT NULL,
	PRIMARY KEY (`RoleID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB