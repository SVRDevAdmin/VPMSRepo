CREATE TABLE `mst_servicescategory` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`Name` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`SubCategoryName` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
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

CREATE TABLE `mst_services` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`CategoryID` INT NULL DEFAULT NULL,
	`Name` VARCHAR(150) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Prices` DECIMAL(18,2) NULL DEFAULT NULL,
	`Duration` DECIMAL(6,2) NULL DEFAULT NULL,
	`Status` INT NULL DEFAULT NULL,
	`Description` TEXT NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Precaution` TEXT NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`BranchID` INT NULL DEFAULT NULL,
	`DoctorInCharge` TEXT NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `mst_producttype` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`TypeName` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
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

CREATE TABLE `mst_product` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`ProductTypeID` INT NULL DEFAULT NULL,
	`BranchID` INT NULL DEFAULT NULL,
	`SKU` VARCHAR(30) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`InventoryName` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Name` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`RecommendedWeight` DECIMAL(6,2) NULL DEFAULT NULL,
	`PricePerQty` DECIMAL(10,2) NULL DEFAULT NULL,
	`Species` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`RecommendedBreed` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Usage` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Description` TEXT NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ImageFilePath` TEXT NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ImageFileName` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `mst_product_status` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`ProductID` INT NULL DEFAULT NULL,
	`StockStatus` INT NULL DEFAULT NULL,
	`QtyInStores` INT NULL DEFAULT NULL,
	`LowStockThreshold` INT NULL DEFAULT NULL,
	`ExpiryDate` DATETIME NULL DEFAULT NULL,
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `txn_stock_transaction` (
	`ID` BIGINT NOT NULL AUTO_INCREMENT,
	`ProductID` INT NULL DEFAULT NULL,
	`SerialNo` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ExpiryDate` DATE NULL DEFAULT NULL,
	`StockInDateTime` DATETIME NULL DEFAULT NULL,
	`StockInBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`StockOutDateTime` DATETIME NULL DEFAULT NULL,
	`StockOutBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

INSERT INTO `mst_servicescategory`
(`Name`,`SubCategoryName`,`Status`,`CreatedDate`,`CreatedBy`)
VALUES
('Surgery','Spaying',1,now(),'System'),
('Surgery','Dental',1,now(),'System'),
('Surgery','Ear',1,now(),'System'),
('Vaccination','Vaccination',1,now(),'System'),
('Vaccination','Deworming',1,now(),'System'),
('Vaccination','Rabbits Vaccination',1,now(),'System'),
('Consultation','Consultation',1,now(),'System'),
('Treatment','Flea Treatment',1,now(),'System');