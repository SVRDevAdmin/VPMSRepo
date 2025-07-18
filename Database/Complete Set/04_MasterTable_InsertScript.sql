/*-------------- MASTERCODEDATA --------------*/
INSERT INTO `mst_mastercodedata`(`CodeGroup`,`CodeID`,`CodeName`,`Description`,`IsActive`,`SeqOrder`,`CreatedDate`,`CreatedBy`)
VALUES
('RoleType', '1','Doctor','Doctor role',1,1,NOW(),'System'),
('RoleType', '2','Clinic Admin','Clinic Admin role',1,2,NOW(),'System'),
('RoleType', '3','User','User role',1,3,NOW(),'System'),
('RoleType', '997','User','User role',1,3,NOW(),'System'),
('RoleType', '998','Superuser','Superuser role',1,4,NOW(),'System'),
('RoleType', '999','Superadmin','Superadmin role',1,5,NOW(),'System'),
('Gender','M','Male','Male gender',1,1,NOW(),'System'),
('Gender','F','Female','Female gender',1,2,NOW(),'System'),
('Status','0','Inactive','Inactive Status',1,1,NOW(),'System'),
('Status','1','Active','Active Status',1,2,NOW(),'System'),
('Species','1','Dog','Dog Species',1,1,NOW(),'System'),
('Species','2','Cat','Cat Species',1,2,NOW(),'System'),
('Color','1','Black','Black Color',1,1,NOW(),'System'),
('Color','2','Brown','Brown Color',1,1,NOW(),'System'),
('Color','3','Grey / Silver','Grey / Silver Color',1,1,NOW(),'System'),
('Color','4','Red','Red Color',1,1,NOW(),'System'),
('Color','5','White / Cream','White / Cream Color',1,1,NOW(),'System'),
('Color','6','Yellow / Gold','Yellow / Gold Color',1,1,NOW(),'System'),
('WeightUnit','kg','Kilogram','Kilogram Unit',1,1,NOW(),'System'),
('WeightUnit','g','Gram','Gram Unit',1,2,NOW(),'System'),
('WeightUnit','mg','Miligram','Miligram Unit',1,3,NOW(),'System'),
('HeightUnit','cm','Centimeter','Centimeter Unit',1,1,NOW(),'System'),
('HeightUnit','m','Meter','Meter Unit',1,2,NOW(),'System');

INSERT INTO mst_mastercodedata(CodeGroup, CodeID, CodeName, Description, IsActive, SeqOrder, createdDate, CreatedBy)
VALUES('NotificationConfig', 'INV_Notification', 'Inv. Notification', 'Enable Notification from Inventory', 1, 1, NOW(), 'SYSTEM');

INSERT INTO mst_mastercodedata(CodeGroup, CodeID, CodeName, Description, IsActive, SeqOrder, createdDate, CreatedBy)
VALUES('NotificationConfig', 'SWUPD_Notification', 'SW UPD Notification', 'Enable Notification from Software Update', 1, 2, NOW(), 'SYSTEM');

/*------- Species -----------*/
INSERT INTO `mst_pets_breed`(`Species`,`Breed`,`Active`,`SeqOrder`,`CreatedDate`,`CreatedBy`)
VALUES
('Dog','Alaskan Malamute',1,1,NOW(),'System'),
('Dog','Afghan Hound',1,2,NOW(),'System'),
('Dog','Bulldog',1,3,NOW(),'System'),
('Dog','Boxer',1,4,NOW(),'System'),
('Dog','Chow Chow',1,5,NOW(),'System'),
('Dog','German Shepherd',1,6,NOW(),'System'),
('Dog','Australian Shepherd',1,7,NOW(),'System'),
('Dog','French Bulldog',1,8,NOW(),'System'),
('Dog','Golden Retriever',1,9,NOW(),'System'),
('Dog','Pomeranian',1,10,NOW(),'System'),
('Dog','Chihuahua',1,11,NOW(),'System'),
('Dog','Sussex Spaniel',1,12,NOW(),'System'),
('Dog','American Bulldog',1,13,NOW(),'System'),
('Dog','Rottweiler',1,14,NOW(),'System'),
('Cat','Siamese cat',1,1,NOW(),'System'),
('Cat','Persian cat',1,2,NOW(),'System'),
('Cat','Abyssinian',1,3,NOW(),'System'),
('Cat','Bombay cat',1,4,NOW(),'System'),
('Cat','Scottish Fold',1,5,NOW(),'System'),
('Cat','Birman',1,6,NOW(),'System'),
('Cat','Siberian cat',1,7,NOW(),'System'),
('Cat','Chartreux',1,8,NOW(),'System');

/*------- Product Type ----------*/
INSERT INTO `mst_producttype`
(`TypeName`,`Status`,`CreatedDate`,`CreatedBy`)
VALUES 
('Vaccination',1,now(),'System'),
('Medication',1,now(),'System'),
('Standard',1,now(),'System'),
('Diagnostic',1,now(),'System'),
('Procedure',1,now(),'System');

/*-------- Services Category -------*/
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
('Treatment','Flea Treatment',1,now(),'System'),
('General','General',1,now(),'System'),
('General','Laboratory',1,now(),'System');

/*------ COUNTRY LIST ---------*/
Insert into mst_countrylist (CountryCode, CountryName, IsActive) Values
 ('AF','Afghanistan',1),
 ('AL','Albania',1),
 ('DZ','Algeria',1),
 ('AD','Andorra',1),
 ('AO','Angola',1),
 ('AG','Antigua and Barbuda',1),
 ('AR','Argentina',1),
 ('AM','Armenia',1),
 ('AT','Austria',1),
 ('AZ','Azerbaijan',1),
 ('BH','Bahrain',1),
 ('BD','Bangladesh',1),
 ('BB','Barbados',1),
 ('BY','Belarus',1),
 ('BE','Belgium',1),
 ('BZ','Belize',1),
 ('BJ','Benin',1),
 ('BT','Bhutan',1),
 ('BO','Bolivia',1),
 ('BA','Bosnia and Herzegovina',1),
 ('BW','Botswana',1),
 ('BR','Brazil',1),
 ('BN','Brunei',1),
 ('BG','Bulgaria',1),
 ('BF','Burkina Faso',1),
 ('BI','Burundi',1),
 ('CV','Cabo Verde',1),
 ('KH','Cambodia',1),
 ('CM','Cameroon',1),
 ('CA','Canada',1),
 ('CF','Central African Republic',1),
 ('TD','Chad',1),
 ('MH','Channel Islands',1),
 ('CL','Chile',1),
 ('CN','China',1),
 ('CO','Colombia',1),
 ('KM','Comoros',1),
 ('CG','Congo',1),
 ('CR','Costa Rica',1),
 ('CI','Côte d''Ivoire',1),
 ('HR','Croatia',1),
 ('CU','Cuba',1),
 ('CY','Cyprus',1),
 ('CZ','Czech Republic',1),
 ('DK','Denmark',1),
 ('DJ','Djibouti',1),
 ('DM','Dominica',1),
 ('DO','Dominican Republic',1),
 ('CD','DR Congo',1),
 ('EC','Ecuador',1),
 ('EG','Egypt',1),
 ('SV','El Salvador',1),
 ('GQ','Equatorial Guinea',1),
 ('ER','Eritrea',1),
 ('EE','Estonia',1),
 ('SZ','Eswatini',1),
 ('ET','Ethiopia',1),
 ('FO','Faeroe Islands',1),
 ('FI','Finland',1),
 ('FR','France',1),
 ('GF','French Guiana',1),
 ('GA','Gabon',1),
 ('GM','Gambia',1),
 ('GE','Georgia',1),
 ('DE','Germany',1),
 ('GH','Ghana',1),
 ('GI','Gibraltar',1),
 ('GR','Greece',1),
 ('GD','Grenada',1),
 ('GT','Guatemala',1),
 ('GQ','Guinea',1),
 ('GW','Guinea-Bissau',1),
 ('GY','Guyana',1),
 ('HT','Haiti',1),
 ('VA','Holy See',1),
 ('HN','Honduras',1),
 ('HK','Hong Kong',1),
 ('HU','Hungary',1),
 ('IS','Iceland',1),
 ('IO','India',1),
 ('ID','Indonesia',1),
 ('IR','Iran',1),
 ('IQ','Iraq',1),
 ('IE','Ireland',1),
 ('IM','Isle of Man',1),
 ('IL','Israel',1),
 ('IT','Italy',1),
 ('JM','Jamaica',1),
 ('JP','Japan',1),
 ('JO','Jordan',1),
 ('KZ','Kazakhstan',1),
 ('KE','Kenya',1),
 ('KW','Kuwait',1),
 ('KG','Kyrgyzstan',1),
 ('LA','Laos',1),
 ('LV','Latvia',1),
 ('LB','Lebanon',1),
 ('LS','Lesotho',1),
 ('LR','Liberia',1),
 ('LY','Libya',1),
 ('LI','Liechtenstein',1),
 ('LT','Lithuania',1),
 ('LU','Luxembourg',1),
 ('MO','Macao',1),
 ('MG','Madagascar',1),
 ('MW','Malawi',1),
 ('MY','Malaysia',1),
 ('MV','Maldives',1),
 ('ML','Mali',1),
 ('MT','Malta',1),
 ('MR','Mauritania',1),
 ('MU','Mauritius',1),
 ('YT','Mayotte',1),
 ('MX','Mexico',1),
 ('MD','Moldova',1),
 ('MC','Monaco',1),
 ('MN','Mongolia',1),
 ('ME','Montenegro',1),
 ('MA','Morocco',1),
 ('MZ','Mozambique',1),
 ('MM','Myanmar',1),
 ('NA','Namibia',1),
 ('NP','Nepal',1),
 ('NL','Netherlands',1),
 ('NI','Nicaragua',1),
 ('NE','Niger',1),
 ('NG','Nigeria',1),
 ('KP','North Korea',1),
 ('MK','North Macedonia',1),
 ('NO','Norway',1),
 ('OM','Oman',1),
 ('PK','Pakistan',1),
 ('PA','Panama',1),
 ('PY','Paraguay',1),
 ('PE','Peru',1),
 ('PH','Philippines',1),
 ('PL','Poland',1),
 ('PT','Portugal',1),
 ('QA','Qatar',1),
 ('RE','Réunion',1),
 ('RO','Romania',1),
 ('RU','Russia',1),
 ('RW','Rwanda',1),
 ('SH','Saint Helena',1),
 ('KN','Saint Kitts and Nevis',1),
 ('LC','Saint Lucia',1),
 ('VC','Saint Vincent and the Grenadines',1),
 ('SM','San Marino',1),
 ('ST','Sao Tome & Principe',1),
 ('SA','Saudi Arabia',1),
 ('SN','Senegal',1),
 ('RS','Serbia',1),
 ('SC','Seychelles',1),
 ('SL','Sierra Leone',1),
 ('SG','Singapore',1),
 ('SK','Slovakia',1),
 ('SI','Slovenia',1),
 ('SO','Somalia',1),
 ('ZA','South Africa',1),
 ('KR','South Korea',1),
 ('SS','South Sudan',1),
 ('ES','Spain',1),
 ('LK','Sri Lanka',1),
 ('PS','State of Palestine',1),
 ('SS','Sudan',1),
 ('SR','Suriname',1),
 ('SE','Sweden',1),
 ('CH','Switzerland',1),
 ('SY','Syria',1),
 ('TW','Taiwan',1),
 ('TJ','Tajikistan',1),
 ('TZ','Tanzania',1),
 ('TH','Thailand',1),
 ('BS','The Bahamas',1),
 ('TL','Timor-Leste',1),
 ('TG','Togo',1),
 ('TT','Trinidad and Tobago',1),
 ('TN','Tunisia',1),
 ('TR','Turkey',1),
 ('TM','Turkmenistan',1),
 ('UG','Uganda',1),
 ('UA','Ukraine',1),
 ('AE','United Arab Emirates',1),
 ('GB','United Kingdom',1),
 ('UM','United States',1),
 ('UY','Uruguay',1),
 ('UZ','Uzbekistan',1),
 ('VE','Venezuela',1),
 ('VN','Vietnam',1),
 ('EH','Western Sahara',1),
 ('YE','Yemen',1),
 ('ZM','Zambia',1),
 ('ZW','Zimbabwe',1);
 
/*----------- Country Code [MY] ----------------------*/
INSERT INTO mst_state(CountryID, State, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_countrylist WHERE CountryCode='MY'), 'Johor', NOW(), 'SYSTEM');

INSERT INTO mst_state(CountryID, State, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_countrylist WHERE CountryCode='MY'), 'Kedah', NOW(), 'SYSTEM');

INSERT INTO mst_state(CountryID, State, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_countrylist WHERE CountryCode='MY'), 'Kelantan', NOW(), 'SYSTEM');

INSERT INTO mst_state(CountryID, State, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_countrylist WHERE CountryCode='MY'), 'Malacca', NOW(), 'SYSTEM');

INSERT INTO mst_state(CountryID, State, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_countrylist WHERE CountryCode='MY'), 'Negeri Sembilan', NOW(), 'SYSTEM');

INSERT INTO mst_state(CountryID, State, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_countrylist WHERE CountryCode='MY'), 'Pahang', NOW(), 'SYSTEM');

INSERT INTO mst_state(CountryID, State, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_countrylist WHERE CountryCode='MY'), 'Penang', NOW(), 'SYSTEM');

INSERT INTO mst_state(CountryID, State, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_countrylist WHERE CountryCode='MY'), 'Perak', NOW(), 'SYSTEM');

INSERT INTO mst_state(CountryID, State, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_countrylist WHERE CountryCode='MY'), 'Perlis', NOW(), 'SYSTEM');

INSERT INTO mst_state(CountryID, State, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_countrylist WHERE CountryCode='MY'), 'Terengganu', NOW(), 'SYSTEM');

INSERT INTO mst_state(CountryID, State, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_countrylist WHERE CountryCode='MY'), 'Sabah', NOW(), 'SYSTEM');

INSERT INTO mst_state(CountryID, State, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_countrylist WHERE CountryCode='MY'), 'Sarawak', NOW(), 'SYSTEM');

INSERT INTO mst_state(CountryID, State, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_countrylist WHERE CountryCode='MY'), 'Putrajaya', NOW(), 'SYSTEM');

INSERT INTO mst_state(CountryID, State, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_countrylist WHERE CountryCode='MY'), 'Labuan', NOW(), 'SYSTEM');

INSERT INTO mst_state(CountryID, State, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_countrylist WHERE CountryCode='MY'), 'Selangor', NOW(), 'SYSTEM');

INSERT INTO mst_state(CountryID, State, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_countrylist WHERE CountryCode='MY'), 'Kuala Lumpur', NOW(), 'SYSTEM');


/*------------------- State -----------------------*/
INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Bakri', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Batu Pahat', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Buloh Kasap', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Chaah', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Johor Bahru', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Kampong Dungun', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Kelapa Sawit', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Kluang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Kota Tinggi', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Kulai', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Labis', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Mersing', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Muar', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Parit Raja', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Pasir Gudang Baru', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Pekan Nenas', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Pontian Kechil', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Segamat', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Sekudai', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Simpang Renggam', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Taman Senai', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Tangkak', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Ulu Tiram', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Yong Peng', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Johor'), 'Taman Johor Jaya', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kedah'), 'Alor Setar', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kedah'), 'Bedong', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kedah'), 'Gurun', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kedah'), 'Jitra', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kedah'), 'Kuah', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kedah'), 'Kuala Kedah', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kedah'), 'Kulim', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kedah'), 'Pokok Sena', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kedah'), 'Sungai Petani', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kelantan'), 'Gua Musang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kelantan'), 'Kampong Kadok', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kelantan'), 'Kampong Pangkal Kalong', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kelantan'), 'Kampung Lemal', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kelantan'), 'Ketereh', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kelantan'), 'Kota Bharu', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kelantan'), 'Pasir Mas', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kelantan'), 'Peringat', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kelantan'), 'Pulai Chondong', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kelantan'), 'Tanah Merah', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kelantan'), 'Tumpat', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Malacca'), 'Alor Gajah', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Malacca'), 'Ayer Keroh', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Malacca'), 'Ayer Molek', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Malacca'), 'Batu Berendam', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Malacca'), 'Bemban', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Malacca'), 'Bukit Baru', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Malacca'), 'Bukit Rambai', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Malacca'), 'Jasin', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Malacca'), 'Klebang Besar', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Malacca'), 'Kuala Sungai Baru', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Malacca'), 'Masjid Tanah', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Malacca'), 'Melaka', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Malacca'), 'Pulau Sebang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Malacca'), 'Sungai Udang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Negeri Sembilan'), 'Bahau', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Negeri Sembilan'), 'Kuala Klawang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Negeri Sembilan'), 'Kuala Pilah', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Negeri Sembilan'), 'Nilai', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Negeri Sembilan'), 'Port Dickson', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Negeri Sembilan'), 'Seremban', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Negeri Sembilan'), 'Tampin', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Pahang'), 'Bentong Town', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Pahang'), 'Bukit Tinggi', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Pahang'), 'Jerantut', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Pahang'), 'Kuala Lipis', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Pahang'), 'Kuantan', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Pahang'), 'Mentekab', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Pahang'), 'Pekan', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Pahang'), 'Raub', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Pahang'), 'Tanah Rata', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Pahang'), 'Temerluh', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Penang'), 'Bayan Lepas', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Penang'), 'Bukit Mertajam', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Penang'), 'Butterworth', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Penang'), 'George Town', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Penang'), 'Juru', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Penang'), 'Kepala Batas', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Penang'), 'Nibong Tebal', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Penang'), 'Perai', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Penang'), 'Permatang Kuching', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Penang'), 'Sungai Ara', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Penang'), 'Tanjung Tokong', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Penang'), 'Tasek Glugor', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Perak'), 'Bagan Serai', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Perak'), 'Batu Gajah', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Perak'), 'Bidor', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Perak'), 'Ipoh', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Perak'), 'Kampar', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Perak'), 'Kuala Kangsar', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Perak'), 'Lumut', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Perak'), 'Pantai Remis', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Perak'), 'Parit Buntar', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Perak'), 'Simpang Empat', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Perak'), 'Sitiawan', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Perak'), 'Taiping', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Perak'), 'Tapah Road', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Perak'), 'Teluk Intan', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Perlis'), 'Arau', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Perlis'), 'Kangar', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Perlis'), 'Kuala Perlis', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Terengganu'), 'Cukai', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Terengganu'), 'Jertih', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Terengganu'), 'Kertih', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Terengganu'), 'Kuala Dungun', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Terengganu'), 'Kuala Terengganu', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Terengganu'), 'Marang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Terengganu'), 'Paka', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sabah'), 'Bandar Labuan', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sabah'), 'Beaufort', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sabah'), 'Donggongon', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sabah'), 'Keningau', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sabah'), 'Kinarut', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sabah'), 'Kota Belud', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sabah'), 'Kota Kinabalu', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sabah'), 'Kudat', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sabah'), 'Lahad Datu', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sabah'), 'Papar', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sabah'), 'Putatan', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sabah'), 'Ranau', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sabah'), 'Sandakan', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sabah'), 'Semporna', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sabah'), 'Tawau', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sarawak'), 'Bintulu', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sarawak'), 'Kapit', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sarawak'), 'Kuching', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sarawak'), 'Limbang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sarawak'), 'Miri', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sarawak'), 'Sarikei', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sarawak'), 'Sibu', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sarawak'), 'Simanggang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Sarawak'), 'Sri Aman', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Putrajaya'), 'Putrajaya', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Labuan'), 'Victoria', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Balakong', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Bangi', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Banting', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Batang Berjuntai', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Batu Arang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Beranang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Cyberjaya', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Jenjarum', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Kajang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Klang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Kuala Selangor', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Kuang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Kundang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Puchong', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Rawang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Sabak Bernam', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Selayang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Semenyih', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Serendah', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Seri Kembangan', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Shah Alam', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Subang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Subang Jaya', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Sungai Besar', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Sungai Pelek', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Tanjung Karang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Selangor'), 'Tanjung Sepat', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kuala Lumpur'), 'Ampang', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kuala Lumpur'), 'Cheras', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kuala Lumpur'), 'Kuala Lumpur', NOW(), 'SYSTEM');

INSERT INTO mst_city(StateID, City, CreatedDate, CreatedBy)
VALUES((SELECT ID FROM mst_state WHERE State='Kuala Lumpur'), 'Sentul', NOW(), 'SYSTEM');


/*-------- LANGUAGE SELECTION --------*/
Insert into `mst_mastercodedata` (CodeGroup, CodeID, CodeName, Description, IsActive, SeqOrder)
Values
('LanguageSelection', 'zh-Hans', 'Chinese, Simplified', '', 1, 1),
('LanguageSelection', 'es', 'Spanish', '', 1, 2),
('LanguageSelection', 'en', 'English', '', 1, 3),
('LanguageSelection', 'vi', 'Vietnamese', '', 1, 4),
('LanguageSelection', 'hi', 'Hindi', '', 1, 5),
('LanguageSelection', 'pt', 'Portugese', '', 1, 6),
('LanguageSelection', 'ru', 'Russian', '', 1, 7),
('LanguageSelection', 'ja', 'Japanese', '', 1, 8),
('LanguageSelection', 'de', 'German', '', 1, 9),
('LanguageSelection', 'zh-Hant', 'Chinese, Traditional', '', 1, 10),
('LanguageSelection', 'id', 'Indonesia', '', 1, 11),
('LanguageSelection', 'ko', 'Korean', '', 1, 12),
('LanguageSelection', 'fr', 'French', '', 1, 13),
('LanguageSelection', 'ms-MY', 'Melayu', '', 1, 13);


/*-------- ORGANIZATION --------*/
INSERT INTO `mst_organisation` (`Level`, `ParentID`, `Name`, `TotalStaff`, `Status`, `CreatedDate`, `CreatedBy`) 
VALUES (0, 0, 'Science Valley Retes', 500, 1, NOW(), 'SYSTEM');

INSERT INTO `mst_organisation` (`Level`, `ParentID`, `Name`, `TotalStaff`, `Status`, `CreatedDate`, `CreatedBy`) 
VALUES (1, 1, 'Science Valley Retest L2', 3000, 1, NOW(), 'SYSTEM');


/* ---- ROLES --------*/
INSERT INTO aspnetroles(Id, NAME, NormalizedName)
VALUES(UUID(), 'Superadmin', 'SUPERADMIN');

INSERT INTO aspnetroles(Id, NAME, NormalizedName)
VALUES(UUID(), 'Customer', 'CUSTOMER');

INSERT INTO mst_roles(`RoleId`, `RoleName`, `RoleType`, `BranchID`, `Status`, `IsAdmin`, `IsDoctor`, `Description`, `CreatedDate`, `CreatedBy`, `OrganizationID`) 
VALUES ((SELECT Id FROM aspnetroles WHERE NAME='Superadmin'), 'Superadmin', 999, NULL, 1, 1, 0, NULL, NOW(), 'SYSTEM', 
(SELECT ID FROM mst_organisation WHERE LEVEL = 0 LIMIT 1));

INSERT INTO mst_roles(`RoleId`, `RoleName`, `RoleType`, `BranchID`, `Status`, `IsAdmin`, `IsDoctor`, `Description`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT Id FROM aspnetroles WHERE NAME='Customer'), 'Customer', 997, NULL, 1, 0, 0, NULL, NOW(), 'SYSTEM');

/*------- Role Permission --------*/
INSERT INTO mst_rolepermissions(RoleID, PermissionKey, IsDeleted, CreatedDate, CreatedBy)
VALUES((SELECT RoleID FROM mst_roles WHERE roleType='999'), 'General.Superadmin', 0, NOW(), 'SYSTEM');

--INSERT INTO mst_rolepermissions(RoleID, PermissionKey, IsDeleted, CreatedDate, CreatedBy)
--VALUES((SELECT RoleID FROM mst_roles WHERE roleType='998'), 'General.Superuser', 0, NOW(), 'SYSTEM');


/*-------- APPOINTMENT GROUPING --------*/
INSERT INTO mst_appointment_grouping(AppointmentGroup, AppointmentSubGroup, AppointmentSubGrpValue, SeqNo, CreatedDate, CreatedBy)
VALUES('AppointmentView', 'Pending', '1', 1, NOW(), 'SYSTEM');

INSERT INTO mst_appointment_grouping(AppointmentGroup, AppointmentSubGroup, AppointmentSubGrpValue, SeqNo, CreatedDate, CreatedBy)
VALUES('AppointmentView', 'Confirmed', '2', 2, NOW(), 'SYSTEM');

INSERT INTO mst_appointment_grouping(AppointmentGroup, AppointmentSubGroup, AppointmentSubGrpValue, SeqNo, CreatedDate, CreatedBy)
VALUES('AppointmentView', 'Past', '3', 3, NOW(), 'SYSTEM');

/*-------- AVATAR ----------*/
INSERT INTO mst_avatar(Species, AvatarImage, ColorCode, STATUS, CreatedDate, CreatedBy)
VALUES('Cat', 'Avatar_Cat_1.png', '#f9dde7', 1, NOW(), 'SYSTEM');

INSERT INTO mst_avatar(Species, AvatarImage, ColorCode, STATUS, CreatedDate, CreatedBy)
VALUES('Cat', 'Avatar_Cat_2.png', '#ffd3a8', 1, NOW(), 'SYSTEM');

INSERT INTO mst_avatar(Species, AvatarImage, ColorCode, STATUS, CreatedDate, CreatedBy)
VALUES('Cat', 'Avatar_Cat_3.png', '#e1cbff', 1, NOW(), 'SYSTEM');

INSERT INTO mst_avatar(Species, AvatarImage, ColorCode, STATUS, CreatedDate, CreatedBy)
VALUES('Dog', 'Avatar_Dog_1.png', '#b8e9fa', 1, NOW(), 'SYSTEM');

INSERT INTO mst_avatar(Species, AvatarImage, ColorCode, STATUS, CreatedDate, CreatedBy)
VALUES('Dog', 'Avatar_Dog_2.png', '#f9dde7', 1, NOW(), 'SYSTEM');

INSERT INTO mst_avatar(Species, AvatarImage, ColorCode, STATUS, CreatedDate, CreatedBy)
VALUES('Dog', 'Avatar_Dog_3.png', '#e1cbff', 1, NOW(), 'SYSTEM');

 
/*-------- ACCESS PERMISSION -------(/
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

-- Invoice & Receipt --
INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Invoices', 'Invoice.Add', 'Create new invoice', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Invoices', 'Invoice.View', 'View invoice / Receipt', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Invoices', 'Invoice.Download', 'Download invoice / Receipt', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy)
VALUES ('Invoices', 'Invoice.Print', 'Print invoice / Receipt', 1, NOW(), 'SYSTEM');

/*------ General -------*/
INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy) 
VALUES ('General', 'General.Superadmin', 'Superadmin', 1, NOW(), 'SYSTEM');

INSERT INTO mst_accesspermission (PermissionGrouping, PermissionKey, PermissionName, IsActive, CreatedDate, CreatedBy) 
VALUES ('General', 'General.Superuser', 'Superuser', 1, NOW(), 'SYSTEM');


/*------- Pets Health Ranges -------- */
INSERT INTO mst_pets_ranges(Species, RangeType, RangeStart, RangeEnd, STATUS, CreatedDate, CreatedBy)
VALUES('Dog', 'BMI', '18.5', '24.9', 1, NOW(), 'SYSTEM');

INSERT INTO mst_pets_ranges(Species, RangeType, RangeStart, RangeEnd, STATUS, CreatedDate, CreatedBy)
VALUES('Cat', 'BMI', '15', '29.9', 1, NOW(), 'SYSTEM');

/*-------- Currency ---------*/
INSERT INTO mst_currency(Country, CurrencyCode, CurrencySymbol, DisplayFormat, STATUS, CreatedDate, CreatedBy)
VALUES('MY', 'MYR', 'RM', 'RM###<Symbol>###', 1, NOW(), 'SYSTEM');

INSERT INTO mst_currency(Country, CurrencyCode, CurrencySymbol, DisplayFormat, STATUS, CreatedDate, CreatedBy)
VALUES('UM', 'USD', '$', '$###<Symbol>###', 1, NOW(), 'SYSTEM');

INSERT INTO mst_currency(Country, CurrencyCode, CurrencySymbol, DisplayFormat, STATUS, CreatedDate, CreatedBy)
VALUES('JP', 'JPY', '¥', '¥###<Symbol>###', 1, NOW(), 'SYSTEM');

/*-------- Profile Avatar --------- */
-- Male
INSERT INTO mst_profile_avatar(EntityGroup, EntitySubGroup, AvatarFileName, AvatarFilePath, STATUS, CreatedDate, CreatedBy)
VALUES('Gender', 'Male', 'MaleProfile_1.png', '/images/profile/male/MaleProfile_1.png', 1, NOW(), 'SYSTEM');

INSERT INTO mst_profile_avatar(EntityGroup, EntitySubGroup, AvatarFileName, AvatarFilePath, STATUS, CreatedDate, CreatedBy)
VALUES('Gender', 'Male', 'MaleProfile_2.png', '/images/profile/male/MaleProfile_2.png', 1, NOW(), 'SYSTEM');

INSERT INTO mst_profile_avatar(EntityGroup, EntitySubGroup, AvatarFileName, AvatarFilePath, STATUS, CreatedDate, CreatedBy)
VALUES('Gender', 'Male', 'MaleProfile_3.png', '/images/profile/male/MaleProfile_3.png', 1, NOW(), 'SYSTEM');

-- Female
INSERT INTO mst_profile_avatar(EntityGroup, EntitySubGroup, AvatarFileName, AvatarFilePath, STATUS, CreatedDate, CreatedBy)
VALUES('Gender', 'Female', 'FemaleProfile_1.png', '/images/profile/Female/FemaleProfile_1.png', 1, NOW(), 'SYSTEM');

INSERT INTO mst_profile_avatar(EntityGroup, EntitySubGroup, AvatarFileName, AvatarFilePath, STATUS, CreatedDate, CreatedBy)
VALUES('Gender', 'Female', 'FemaleProfile_2.png', '/images/profile/Female/FemaleProfile_2.png', 1, NOW(), 'SYSTEM');

INSERT INTO mst_profile_avatar(EntityGroup, EntitySubGroup, AvatarFileName, AvatarFilePath, STATUS, CreatedDate, CreatedBy)
VALUES('Gender', 'Female', 'FemaleProfile_3.png', '/images/profile/Female/FemaleProfile_3.png', 1, NOW(), 'SYSTEM');

