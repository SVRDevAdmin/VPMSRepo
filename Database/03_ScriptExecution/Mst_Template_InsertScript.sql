/*----------- Email : New Appointment Creation --------------*/
INSERT INTO mst_template(TemplateType, TemplateCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES('Email Notification', 'VPMS_EN001', 'New Appointment Creation', '', 
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN001'),
'de', 'Neue Terminerstellung',
'<!DOCTYPE html>
<html>
 	<body>
		Sehr geehrter ###<customer>###, </br></br>

		Wir freuen uns, Ihnen mitteilen zu können, dass Ihr Termin erfolgreich erstellt wurde!</br></br>

		Nachfolgend finden Sie die Termindetails:</br></br>
		Datum: ###<appointmetdate>###</br>
		Uhrzeit: ###<appointmenttime>###</br>
		Name des Haustiers: ###<petname>###</br>
		Dienstleistungen: ###<services>###</br>
		Arzt: ###<doctorname>###</br></br>

		Bitte antworten Sie nicht auf diese E-Mail, da sie automatisch generiert wird.</br></br>

		Mit freundlichen Grüßen,</br>
		VPMS-Team</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN001'),
'en', 'New Appointment Creation',
'<!DOCTYPE html>
<html>
 	<body>
 		Dear ###<customer>###, </br></br>

 		We are pleased to inform you that your appointment has been successfully created!</br></br>

 		Below are appointment details:</br></br>
 		Date: ###<appointmetdate>###</br>
		Time: ###<appointmenttime>###</br>
 		Pet Name: ###<petname>###</br>
		Services: ###<services>###</br>
		Doctor: ###<doctorname>###</br></br>

 		Please refrain from replying to this email as it is auto-generated.</br></br>

 		Best regards,</br>
 		VPMS Team</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN001'),
'es', 'Creación de nueva cita',
'<!DOCTYPE html>
<html>
 	<body>
		Estimado ###<customer>###, </br></br>

		Nos complace informarle que su cita se ha creado correctamente.</br></br>

		A continuación, se muestran los detalles de la cita:</br></br>
		Fecha: ###<appointmetdate>###</br>
		Hora: ###<appointmenttime>###</br>
		Nombre de la mascota: ###<petname>###</br>
		Servicios: ###<services>###</br>
		Médico: ###<doctorname>###</br></br>

		Absténgase de responder a este correo electrónico, ya que se genera automáticamente.</br></br>

		Atentamente,</br>
		Equipo de VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN001'),
'fr', 'Création d\'un nouveau rendez-vous',
'<!DOCTYPE html>
<html>
 	<body>
		Cher ###<customer>###, </br></br>

		Nous avons le plaisir de vous informer que votre rendez-vous a été créé avec succès !</br></br>

		Vous trouverez ci-dessous les détails du rendez-vous :</br></br>
		Date : ###<appointmetdate>###</br>
		Heure : ###<appointmenttime>###</br>
		Nom de l\'animal : ###<petname>###</br>
		Services : ###<services>###</br>
		Médecin : ###<doctorname>###</br></br>

		Veuillez vous abstenir de répondre à cet e-mail car il est généré automatiquement.</br></br>

		Cordialement,</br>
		L\'équipe VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN001'),
'hi', 'नई नियुक्ति का निर्माण',
'<!DOCTYPE html>
<html>
 	<body>
		प्रिय ###<customer>###, </br></br>

		हमें आपको यह बताते हुए खुशी हो रही है कि आपकी नियुक्ति सफलतापूर्वक बना दी गई है!</br></br>

		नीचे नियुक्ति विवरण दिए गए हैं:</br></br>
		दिनांक: ###<appointmetdate>###</br>
		समय: ###<appointmenttime>###</br>
		पालतू जानवर का नाम: ###<petname>###</br>
		सेवाएँ: ###<services>###</br>
		डॉक्टर: ###<doctorname>###</br></br>

		कृपया इस ईमेल का उत्तर देने से बचें क्योंकि यह स्वतः जनरेट किया गया है।</br></br>

		सादर,</br>
		VPMS टीम</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN001'),
'id', 'Pembuatan Janji Temu Baru',
'<!DOCTYPE html>
<html>
 	<body>
		Kepada ###<customer>###, </br></br>

		Dengan senang hati kami informasikan bahwa janji temu Anda telah berhasil dibuat!</br></br>

		Berikut adalah detail janji temu:</br></br>
		Tanggal: ###<appointmetdate>###</br>
		Waktu: ###<appointmenttime>###</br>
		Nama Hewan Peliharaan: ###<petname>###</br>
		Layanan: ###<services>###</br>
		Dokter: ###<doctorname>###</br></br>

		Harap jangan membalas email ini karena email ini dibuat secara otomatis.</br></br>

		Salam,</br>
		Tim VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN001'),
'ja', '新しい予定の作成',
'<!DOCTYPE html>
<html>
 	<body>
		###<customer>### 様</br></br>

		ご予約が正常に作成されましたことをお知らせいたします。</br></br>

		以下がご予約の詳細です:</br></br>
		日付: ###<appointmetdate>###</br>
		時刻: ###<appointmenttime>###</br>
		ペットの名前: ###<petname>###</br>
		サービス: ###<services>###</br>
		医師: ###<doctorname>###</br></br>

		このメールは自動生成されるため、返信は控えてください。</br></br>

		よろしくお願いいたします。</br>

		VPMS チーム</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN001'),
'ko', '새로운 약속 생성',
'<!DOCTYPE html>
<html>
 	<body>
		친애하는 ###<customer>###님, </br></br>

		예약이 성공적으로 생성되었음을 알려드리게 되어 기쁩니다!</br></br>

		예약 세부 정보는 다음과 같습니다.</br></br>
		날짜: ###<appointmetdate>###</br>
		시간: ###<appointmenttime>###</br>
		반려동물 이름: ###<petname>###</br>
		서비스: ###<services>###</br>
		의사: ###<doctorname>###</br></br>

		이 이메일은 자동 생성되므로 답장하지 마십시오.</br></br>

		감사합니다.</br>
		VPMS 팀</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN001'),
'pt', 'Criação de novo compromisso',
'<!DOCTYPE html>
<html>
 	<body>
		Estimado ###<customer>###, </br></br>

		Temos o prazer de informar que o seu agendamento foi criado com sucesso!</br></br>

		Abaixo seguem os detalhes do agendamento:</br></br>
		Data: ###<appointmetdate>###</br>
		Hora: ###<appointmenttime>###</br>
		Nome do animal de estimação: ###<petname>###</br>
		Serviços: ###<serviços>###</br>
		Médico: ###<doctorname>###</br></br>

		Evite responder a este e-mail, pois é gerado automaticamente.</br></br>

		Atenciosamente,</br>
		Equipa VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN001'),
'ru', 'Создание новой встречи',
'<!DOCTYPE html>
<html>
 	<body>
		Уважаемый ###<customer>###, </br></br>

		Мы рады сообщить вам, что ваш прием был успешно создан!</br></br>

		Ниже приведены данные о приеме:</br></br>
		Дата: ###<appointmetdate>###</br>
		Время: ###<appointmenttime>###</br>
		Имя питомца: ###<petname>###</br>
		Услуги: ###<services>###</br>
		Врач: ###<doctorname>###</br></br>

		Пожалуйста, воздержитесь от ответа на это письмо, так как оно создается автоматически.</br></br>

		С наилучшими пожеланиями,</br>
		Команда VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN001'),
'vi', 'Tạo cuộc hẹn mới',
'<!DOCTYPE html>
<html>
 	<body>
		Kính gửi ###<customer>###, </br></br>

		Chúng tôi vui mừng thông báo rằng cuộc hẹn của bạn đã được tạo thành công!</br></br>

		Dưới đây là thông tin chi tiết về cuộc hẹn:</br></br>
		Ngày: ###<appointmetdate>###</br>
		Thời gian: ###<appointmenttime>###</br>
		Tên thú cưng: ###<petname>###</br>
		Dịch vụ: ###<services>###</br>
		Bác sĩ: ###<doctorname>###</br></br>

		Vui lòng không trả lời email này vì nó được tạo tự động.</br></br>

		Trân trọng,</br>
		Nhóm VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN001'),
'zh-Hant', '新約會創建',
'<!DOCTYPE html>
<html>
 	<body>
		親愛的 ###<customer>###，</br></br>

		我們很高興地通知您，您的預約已成功建立！

		以下是預約詳情：</br></br>
		日期：###<appointmetdate>###</br>
		時間：###<appointmenttime>###</br>
		寵物名稱：###<petname>###</br>
		服務：###<services>###</br>
		醫師：###<doctorname>###</br></br>

		請不要回覆此電子郵件，因為它是自動產生的。</br></br>

		謹致問候，</br>
		VPMS團隊</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN001'),
'zh-Hans', '创建新预约',
'<!DOCTYPE html>
<html>
 	<body>
		尊敬的 ###<customer>###，</br></br>

		我们很高兴地通知您，您的预约已成功创建！</br></br>

		以下是预约详情：</br></br>
		日期：###<appointmetdate>###</br>
		时间：###<appointmenttime>###</br>
		宠物名称：###<petname>###</br>
		服务：###<services>###</br>
		医生：###<doctorname>###</br></br>

		请不要回复此电子邮件，因为它是自动生成的。</br></br>

		此致，</br>
		VPMS 团队</br>
 	</body>
</html>',
NOW(), 'SYSTEM');


/*---------- Email : New Account Creation ----------------*/
INSERT INTO mst_template(TemplateType, TemplateCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES('Email Notification', 'VPMS_EN003', 'New Login Account Creation', '', NOW(), 'SYSTEM');


INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'en', 'New Login Account Creation',
'<!DOCTYPE html>
<html>
	<body>
		Dear ###<user_fullname>###, </br></br>

		We are pleased to inform you that your account has been successfully created!</br></br>

		Below are your login details:</br>
		Login ID: ###<userlogin_id>###</br>
		Temporary Password: ###<user_password>###</br></br>

		Please refrain from replying to this email as it is auto-generated.</br></br>

		Best regards,</br>
		VetVitals Team</br>
	</body>
</html>', 
NOW(), 'SYSTEM');


INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'zh-Hant', '建立新登入帳戶',
'<!DOCTYPE html>
<html>
	<body>
	  親愛的###<user_fullname>###，</br></br>

	  我們很高興地通知您，您的帳戶已成功建立！

	  以下是您的登入詳細資訊：</br>
	  登入 ID：###<userlogin_id>###</br>
	  臨時密碼：###<user_password>###</br></br>

	  請不要回覆此電子郵件，因為它是自動產生的。</br></br>

	  此致敬禮，</br>
	  VetVitals 團隊
	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'zh-Hans', '创建新登录账户',
'<!DOCTYPE html>
<html>
	<body>
		亲爱的 ###<user_fullname>###，</br></br>
		
		我们很高兴地通知您，您的帐户已成功创建！</br></br>
		
		以下是您的登录详细信息：</br>
		登录 ID：###<userlogin_id>###</br>
		临时密码：###<user_password>###</br></br>
		
		请不要回复此电子邮件，因为它是自动生成的。</br></br>
		
		此致，</br>
		VetVitals 团队</br>
	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'de', 'Neues Login-Konto erstellen',
'<!DOCTYPE html>
<html>
	<body>
		Sehr geehrter ###<user_fullname>###, </br></br>
		
		Wir freuen uns, Ihnen mitteilen zu können, dass Ihr Konto erfolgreich erstellt wurde!</br></br>
		
		Nachfolgend finden Sie Ihre Anmeldedaten:</br>
		Anmelde-ID: ###<userlogin_id>###</br>
		Temporäres Passwort: ###<user_password>###</br></br>
		
		Bitte antworten Sie nicht auf diese E-Mail, da sie automatisch generiert wird.</br></br>
		
		Mit freundlichen Grüßen,</br>
		VetVitals-Team</br>
	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'es', 'Creación de una nueva cuenta de inicio de sesión',
'<!DOCTYPE html>
<html>
	<body>
		Estimado ###<user_fullname>###, </br></br>
		
		Nos complace informarle que su cuenta se ha creado correctamente.</br></br>
		
		A continuación, se muestran sus datos de inicio de sesión:</br>
		ID de inicio de sesión: ###<userlogin_id>###</br>
		Contraseña temporal: ###<user_password>###</br></br>
		
		No responda a este correo electrónico, ya que se genera automáticamente.</br></br>
		
		Atentamente,</br>
		Equipo de VetVitals</br>
	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'fr', 'Création d''un nouveau compte de connexion',
'<!DOCTYPE html>
<html>
	<body>
		Cher ###<user_fullname>###, </br></br>
		
		Nous avons le plaisir de vous informer que votre compte a été créé avec succès !</br></br>
		
		Vous trouverez ci-dessous vos identifiants de connexion :</br>
		ID de connexion : ###<userlogin_id>###</br>
		Mot de passe temporaire : ###<user_password>###</br></br>
		
		Veuillez vous abstenir de répondre à cet e-mail car il est généré automatiquement.</br></br>
		
		Cordialement,</br>
		L''équipe VetVitals</br>
	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'hi', 'नया लॉगिन खाता निर्माण',
'<!DOCTYPE html>
<html>
	<body>
		प्रिय ###<user_fullname>###, </br></br>
		
		हमें आपको यह बताते हुए खुशी हो रही है कि आपका खाता सफलतापूर्वक बना लिया गया है!</br></br>
		
		नीचे आपके लॉगिन विवरण दिए गए हैं:</br>
		लॉगिन आईडी: ###<userlogin_id>###</br>
		अस्थायी पासवर्ड: ###<user_password>###</br></br>
		
		कृपया इस ईमेल का उत्तर देने से बचें क्योंकि यह स्वतः जनरेट किया गया है।</br></br>
		
		सादर,</br>
		VetVitals टीम</br>
	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'id', 'Pembuatan Akun Login Baru',
'<!DOCTYPE html>
<html>
	<body>
		Kepada ###<user_fullname>###, </br></br>

		Dengan senang hati kami informasikan bahwa akun Anda telah berhasil dibuat!</br></br>

		Berikut adalah detail login Anda:</br>
		ID Login: ###<userlogin_id>###</br>
		Kata Sandi Sementara: ###<user_password>###</br></br>

		Harap jangan membalas email ini karena email ini dibuat secara otomatis.</br></br>

		Salam,</br>
		Tim VetVitals</br>
	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'ja', '新しいログインアカウントの作成',
'<!DOCTYPE html>
<html>
	<body>
		###<user_fullname>### 様</br></br>

		アカウントが正常に作成されたことをお知らせいたします。</br></br>

		ログイン情報は以下の通りです:</br>
		ログイン ID: ###<userlogin_id>###</br>
		仮パスワード: ###<user_password>###</br></br>

		このメールは自動生成されるため、返信しないでください。</br></br>

		よろしくお願いいたします。</br>

		VetVitals チーム</br>
	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'ko', '새로운 로그인 계정 생성',
'<!DOCTYPE html>
<html>
	<body>
		친애하는 ###<user_fullname>###님, </br></br>
		
		귀하의 계정이 성공적으로 생성되었음을 알려드리게 되어 기쁩니다!</br></br>
		
		귀하의 로그인 정보는 다음과 같습니다.</br>
		로그인 ID: ###<userlogin_id>###</br>
		임시 비밀번호: ###<user_password>###</br></br>
		
		이 이메일은 자동 생성되므로 답장하지 마십시오.</br></br>
		
		감사합니다.</br>
		VetVitals 팀</br>
	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'pt', 'Criação de nova conta de login',
'<!DOCTYPE html>
<html>
	<body>
	  Caro ###<user_fullname>###, </br></br>

	  Temos o prazer de informar que a sua conta foi criada com sucesso!</br></br>

	  Abaixo estão os seus dados de login:</br>
	  ID de login: ###<userlogin_id>###</br>
	  Password temporária: ###<user_password>###</br></br>

	  Por favor, evite responder a este e-mail, pois é gerado automaticamente.</br></br>

	  Atenciosamente,</br>
	  Equipa VetVitals
	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'ru', 'Создание новой учетной записи',
'<!DOCTYPE html>
<html>
	<body>
		Уважаемый ###<user_fullname>###, </br></br>

		Мы рады сообщить вам, что ваша учетная запись была успешно создана!</br></br>

		Ниже приведены ваши данные для входа:</br>
		Идентификатор входа: ###<userlogin_id>###</br>
		Временный пароль: ###<user_password>###</br></br>

		Пожалуйста, воздержитесь от ответа на это письмо, так как оно генерируется автоматически.</br></br>

		С наилучшими пожеланиями,</br>
		Команда VetVitals</br>
	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'vi', 'Tạo tài khoản đăng nhập mới',
'<!DOCTYPE html>
<html>
	<body>
		Kính gửi ###<user_fullname>###, </br></br>

		Chúng tôi vui mừng thông báo rằng tài khoản của bạn đã được tạo thành công!</br></br>

		Dưới đây là thông tin chi tiết về thông tin đăng nhập của bạn:</br>
		ID đăng nhập: ###<userlogin_id>###</br>
		Mật khẩu tạm thời: ###<user_password>###</br></br>

		Vui lòng không trả lời email này vì nó được tạo tự động.</br></br>

		Trân trọng,</br>
		Đội ngũ VetVitals</br>
	</body>
</html>', 
NOW(), 'SYSTEM');





