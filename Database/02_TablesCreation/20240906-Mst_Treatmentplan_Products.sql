CREATE TABLE `mst_treatmentplan_products` (
	`ID` INT NOT NULL AUTO_INCREMENT, 
	`PlanID` INT NULL DEFAULT NULL,
	`ProductID` INT NULL DEFAULT NULL,
	`ProductName` VARCHAR(300) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',    
	`Units` INT NULL DEFAULT NULL,
	`PricePerQty` DECIMAL(10,2) NULL DEFAULT NULL, 
	`TotalPrice` DECIMAL(10,2) NULL DEFAULT NULL, 
	`IsDeleted` INT NULL DEFAULT NULL,
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

INSERT INTO `vpmsdb`.`mst_producttype`
(`TypeName`,`Status`,`CreatedDate`,`CreatedBy`)
VALUES 
('Vaccination',1,now(),'System'),
('Medication',1,now(),'System'),
('Standard',1,now(),'System'),
('Diagnostic',1,now(),'System'),
('Procedure',1,now(),'System');
