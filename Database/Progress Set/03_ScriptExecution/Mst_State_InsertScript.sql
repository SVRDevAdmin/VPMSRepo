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


