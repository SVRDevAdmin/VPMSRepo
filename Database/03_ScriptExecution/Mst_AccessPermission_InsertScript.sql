/*------ Dashboard -------*/
INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy) 
VALUES ('Dashboard', 'Dashboard.View', 'View Dashboard', 1, NOW(), 'SYSTEM');

/*------ Patients --------*/
INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy) 
VALUES ('Patients', 'Patients.Add', 'Add New Patient', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Patients', 'PatientListing.View', 'View Patient Listing', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Patients', 'PatientDetails.View', 'View Patient Details', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Patients', 'PatientDetails.Edit', 'Edit Patient Details', 1, NOW(), 'SYSTEM');

/*--------- Appointments ------*/
INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Appointments', 'Appointments.View', 'View Appointments', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Appointments', 'Appointments.Add', 'Add New Appointments', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Appointments', 'AppointmentDetails.View', 'Add Appointment Details', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Appointments', 'AppointmentDetails.Edit', 'Edit Appointment Details', 1, NOW(), 'SYSTEM');

/*----------- Customer Services -------*/
INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('CustomerService', 'CustomerService.View', 'View Customer Service', 1, NOW(), 'SYSTEM');

/*----------- Inventory ---------------*/
INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Inventory', 'Inventory.Add', 'Add New Inventory', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Inventory', 'InventoryListing.Download', 'Download Inventory Listing', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Inventory', 'InventoryListing.Print', 'Print Inventory Listing', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Inventory', 'InventoryListing.View', 'View Inventory Listing', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Inventory', 'InventoryDetails.View', 'View Inventory Details', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Inventory', 'InventoryDetails.Edit', 'Edit Inventory Details', 1, NOW(), 'SYSTEM');

/*----------- Service ---------------*/
INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Service', 'Service.Add', 'Add New Service', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Service', 'ServiceListing.Download', 'Download Service Listing', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Service', 'ServiceListing.Print', 'Print Service Listing', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Service', 'ServiceListing.View', 'View Service Listing', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Service', 'ServiceDetails.View', 'View Service Details', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Service', 'ServiceDetails.Edit', 'Edit Service Details', 1, NOW(), 'SYSTEM');

/*----------- Patient Treatment Plan ---------------*/
INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('TreatmentPlan', 'TreatmentPlan.Add', 'Add Treatment Plan', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('TreatmentPlan', 'TreatmentPlan.Edit', 'Edit Treatment Plan', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('TreatmentPlan', 'TreatmentPlan.View', 'View Treatment Plan', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('TreatmentPlan', 'TreatmentPlan.Status', 'Enable/Disabled Treatment Plan', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('TreatmentPlan', 'TreatmentListing.View', 'View Treatment Listing', 1, NOW(), 'SYSTEM');

/*----------- Access Control ---------------*/
INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Access Control', 'OrganizationListing.View', 'View Organization Listing', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Access Control', 'OrganizationDetails.View', 'View Organization Details', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Access Control', 'OrganizationDetails.Add', 'Add Organization', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Access Control', 'OrganizationDetails.Edit', 'Edit Organization Details', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Access Control', 'Branch.Add', 'Add Branch', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Access Control', 'Branch.Edit', 'Edit Branch', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Access Control', 'StaffListing.View', 'View Staff Listing', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Access Control', 'Staff.Add', 'Add Staff', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Access Control', 'StaffDetails.View', 'View Staff Details', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Access Control', 'StaffDetails.Edit', 'Edit Staff Details', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Access Control', 'StaffDetails.Delete', 'Delete Staff Details', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Access Control', 'RoleListing.View', 'View Role Listing', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Access Control', 'Role.Add', 'Add Role', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Access Control', 'RoleDetails.View', 'View Role Details', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Access Control', 'RoleDetails.Edit', 'Edit Role Details', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Access Control', 'Role.Delete', 'Delete Role', 1, NOW(), 'SYSTEM');

/*------ General -------*/
INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy) 
VALUES ('General', 'General.Superadmin', 'Superadmin', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy) 
VALUES ('General', 'General.Superuser', 'Superuser', 1, NOW(), 'SYSTEM');