CREATE TABLE `Txn_TreatmentPlan_Services` (
    `ID` INT NOT NULL AUTO_INCREMENT,
    `PlanID` INT NULL DEFAULT NULL,
    `ServiceID` INT NULL DEFAULT NULL,
    `ServiceName` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Price` DECIMAL(10,2) NULL DEFAULT NULL, 
	`Discount` DECIMAL(10,2) NULL DEFAULT NULL, 
	`TotalPrice` DECIMAL(10,2) NULL DEFAULT NULL, 
    `IsDeleted` INT NULL DEFAULT NULL,
    `CreatedDate` DATETIME NULL DEFAULT NULL,
    `CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
    `UpdatedDate` DATETIME NULL DEFAULT NULL,
    `UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
    PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;