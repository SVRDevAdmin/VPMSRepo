CREATE TABLE `txn_notifications` (
	`ID` BIGINT NOT NULL AUTO_INCREMENT,
	`NotificationGroup` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`NotificationType` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Title` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Content` TEXT NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`CreatedDate` DATETIME NULL DEFAULT NULL,
	`CreatedBy` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `txn_notifiction_receiver` (
	`ID` BIGINT NOT NULL AUTO_INCREMENT,
	`NotificationID` BIGINT NULL DEFAULT NULL,
	`TargetUser` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Status` INT NULL DEFAULT NULL,
	`MsgReadDateTime` DATETIME NULL DEFAULT NULL,
	`MsgDeletedDateTime` DATETIME NULL DEFAULT NULL,
	`UpdatedDate` DATETIME NULL DEFAULT NULL,
	`UpdatedBy` DATETIME NULL DEFAULT NULL,
	PRIMARY KEY (`ID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

CREATE INDEX IX_NotiGroup_CreatedDate
On Txn_Notifications (NotificationGroup, CreatedDate DESC);

CREATE INDEX IX_NotiGroup
On Txn_Notifications (NotificationGroup);

CREATE INDEX IX_Notification_OrderSorting
On Txn_Notifications (CreatedDate DESC);