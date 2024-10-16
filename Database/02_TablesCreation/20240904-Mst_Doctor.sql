CREATE TABLE `mst_doctor` (
    `ID` INT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
    `BranchID` INT NULL DEFAULT NULL,
    `Gender` VARCHAR(1) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
    `LicenseNo` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
    `Designation` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
    `Specialty` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
    `System_ID` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
    `IsDeleted` INT NULL DEFAULT NULL,
    `CreatedDateTimestamp` DATETIME NULL DEFAULT NULL,
    `CreatedDate` DATETIME NULL DEFAULT NULL,
    `CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
    `UpdatedDateTimestamp` DATETIME NULL DEFAULT NULL,
    `UpdatedDate` DATETIME NULL DEFAULT NULL,
    `UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
    PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

INSERT INTO `vpmsdb`.`mst_doctor`
(`Name`,`Gender`,`LicenseNo`,`Designation`,`Specialty`,`System_ID`,`IsDeleted`,`CreatedDate`,`CreatedBy`,`BranchID`,`CreatedDateTimestamp`)
VALUES
('Park Ji-hoon','M','124567890','Senior Doctor','Surgery','1234567890',0,now(),'System',1,now()),
('Lee Soo-jin','M','124567890','Senior Doctor','Surgery','1234567890',0,now(),'System',1,now()),
('Choi Eun-woo','M','124567890','Senior Doctor','Surgery','1234567890',0,now(),'System',1,now()),
('Jung Hye-jin','M','124567890','Senior Doctor','Surgery','1234567890',0,now(),'System',1,now()),
('Hang Don-hyun','M','124567890','Senior Doctor','Surgery','1234567890',0,now(),'System',1,now()),
('Kang Ye-jin','M','124567890','Senior Doctor','Surgery','1234567890',0,now(),'System',1,now()),
('Seo Joon-young','M','124567890','Senior Doctor','Surgery','1234567890',0,now(),'System',1,now()),
('Yoon Mi-rae','M','124567890','Senior Doctor','Surgery','1234567890',0,now(),'System',1,now()),
('Shin Hae-won','M','124567890','Senior Doctor','Surgery','1234567890',0,now(),'System',1,now());