CREATE TABLE `Mst_Currency` (
    `ID` INT NOT NULL AUTO_INCREMENT,
    `Country` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
    `CurrencySymbol` VARCHAR(5) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
    `Status` INT NULL DEFAULT NULL,
    `CreatedDate` DATETIME NULL DEFAULT NULL,
    `CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
    `UpdatedDate` DATETIME NULL DEFAULT NULL,
    `UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
    PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;