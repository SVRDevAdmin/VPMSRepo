CREATE TABLE `txn_notifications` (
	`ID` BIGINT NOT NULL AUTO_INCREMENT,
	`NotificationGroup` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`NotificationType` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Title` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Content` TEXT NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE,
	INDEX `IX_NotiGroup` (`NotificationGroup`) USING BTREE,
	INDEX `IX_NotiGroup_CreatedDate` (`NotificationGroup`, `CreatedDate` DESC) USING BTREE,
	INDEX `IX_Notification_OrderSorting` (`CreatedDate` DESC) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `txn_notification_receiver` (
	`ID` BIGINT NOT NULL AUTO_INCREMENT,
	`NotificationID` BIGINT NULL DEFAULT NULL,
	`TargetUser` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Status` INT NULL DEFAULT NULL,
	`MsgReadDateTime` DATETIME NULL DEFAULT NULL,
	`MsgDeletedDateTime` DATETIME NULL DEFAULT NULL,
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE,
	INDEX `IX_NotiID_UserID_Status` (`NotificationID`, `TargetUser`, `Status`) USING BTREE,
	INDEX `IX_NotiID_UserID` (`NotificationID`, `TargetUser`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `mst_notification_receiver_config` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`NotificationGroup` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`RoleID` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`NotificationSend` INT NULL DEFAULT NULL,
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE,
	INDEX `IX_NotifGroup_RoleID` (`NotificationGroup`, `RoleID`) USING BTREE,
	INDEX `IX_NotifGroup` (`NotificationGroup`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;


