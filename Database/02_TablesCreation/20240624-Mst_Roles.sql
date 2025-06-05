CREATE TABLE `mst_roles` (
	`RoleID` VARCHAR(255) NOT NULL COLLATE 'utf8mb4_general_ci',
	`RoleName` VARCHAR(100) NOT NULL COLLATE 'utf8mb4_general_ci',
	`RoleType` INT NOT NULL,
	`BranchID` INT NULL DEFAULT NULL,
	`Status` INT NOT NULL,
	`IsAdmin` INT NULL DEFAULT NULL,
	`IsDoctor` INT NULL DEFAULT NULL,
	`Description` VARCHAR(255) NULL DEFAULT NULL,
	`CreatedDate` DATETIME NOT NULL,
	`CreatedBy` VARCHAR(50) NOT NULL COLLATE 'utf8mb4_general_ci',
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`RoleID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB


/* ---- aspnetroles --------*/
INSERT INTO aspnetroles(Id, NAME, NormalizedName)
VALUES(UUID(), 'Superadmin', 'SUPERADMIN');

--INSERT INTO aspnetroles(Id, NAME, NormalizedName)
--VALUES(UUID(), 'Superuser', 'SUPERUSER');

INSERT INTO aspnetroles(Id, NAME, NormalizedName)
VALUES(UUID(), 'Customer', 'CUSTOMER');

/*------- mst_roles ---------*/
INSERT INTO mst_roles(`RoleId`, `RoleName`, `RoleType`, `BranchID`, `Status`, `IsAdmin`, `IsDoctor`, `Description`, `CreatedDate`, `CreatedBy`, `OrganizationID`) 
VALUES ((SELECT Id FROM aspnetroles WHERE NAME='Superadmin'), 'Superadmin', 999, NULL, 1, 1, 0, NULL, NOW(), 'SYSTEM', 
(SELECT ID FROM mst_organisation WHERE LEVEL = 0 LIMIT 1));

--INSERT INTO mst_roles(`RoleId`, `RoleName`, `RoleType`, `BranchID`, `Status`, `IsAdmin`, `IsDoctor`, `Description`, `CreatedDate`, `CreatedBy`) 
--VALUES ((SELECT Id FROM aspnetroles WHERE NAME='Superuser'), 'Superuser', 998, NULL, 1, 1, 0, NULL, NOW(), 'SYSTEM');

INSERT INTO mst_roles(`RoleId`, `RoleName`, `RoleType`, `BranchID`, `Status`, `IsAdmin`, `IsDoctor`, `Description`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT Id FROM aspnetroles WHERE NAME='Customer'), 'Customer', 997, NULL, 1, 0, 0, NULL, NOW(), 'SYSTEM');


