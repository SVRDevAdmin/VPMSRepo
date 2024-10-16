CREATE TABLE `mst_appointment` (
	`AppointmentID` BIGINT NOT NULL AUTO_INCREMENT,
	`UniqueIDKey` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`BranchID` INT NULL DEFAULT NULL,
	`ApptDate` DATE NULL DEFAULT NULL,
	`ApptStartTime` TIME NULL DEFAULT NULL,
	`ApptEndTime` TIME NULL DEFAULT NULL,
	`OwnerID` BIGINT NULL DEFAULT NULL,
	`PetID` BIGINT NULL DEFAULT NULL,
	`Status` INT NULL DEFAULT NULL,
	`EmailNotify` BIT(1) NULL DEFAULT NULL,
	`InchargeDoctor` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`AppointmentID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `mst_appointment_services` (
	`ID` BIGINT NOT NULL AUTO_INCREMENT,
	`ApptID` BIGINT NULL DEFAULT NULL,
	`ServicesID` BIGINT NULL DEFAULT NULL,
	`IsDeleted` BIT(1) NOT NULL DEFAULT 0,
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
AUTO_INCREMENT=19
;


CREATE INDEX IX_ValidationDocAppt
ON mst_appointment (BranchID, STATUS, InchargeDoctor, ApptDate, ApptStartTime, ApptEndTime);

CREATE INDEX IX_ValidationOwnerAppt
ON mst_appointment (BranchID, STATUS, OwnerID, PetID, ApptDate, ApptStartTime, ApptEndTime);

CREATE INDEX IX_AppointmentView
ON mst_appointment (PetID, OwnerID, ApptDate, STATUS, InchargeDoctor);

CREATE INDEX IX_AppointmentView_Sort
ON mst_appointment (ApptStartTime);

CREATE INDEX IX_AppointmentView_Services
ON mst_appointment_Services (ServicesID, IsDeleted);

CREATE INDEX IX_API_ApptByCreatedDate
ON mst_appointment (AppointmentID, PetID, OwnerID, CreatedDate, UpdatedDate);

CREATE INDEX IX_API_ApptByUniqueID
ON mst_appointment (AppointmentID, PetID, OwnerID, UniqueIDKey);

