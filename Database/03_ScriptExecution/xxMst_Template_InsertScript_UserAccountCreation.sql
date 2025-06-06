/*----------- Email : PMS Account creation --------------*/
INSERT INTO `mst_template` (`TemplateType`, `TemplateCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ('Email Notification', 'VPMS_EN010', 'Account Creation', '', NOW(), 'SYSTEM');


INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'), 
'en', 'New Login Account Creation', 
'<!DOCTYPE html>\r\n<html>\r\n	<body>\r\n		Dear ###<user_fullname>###, </br></br>\r\n\r\n		We are pleased to inform you that your account has been successfully created!</br></br>\r\n\r\n		Below are your login details:</br>\r\n		Login ID: ###<userlogin_id>###</br>\r\n		Temporary Password: ###<user_password>###</br></br>\r\n\r\n		Please refrain from replying to this email as it is auto-generated.</br></br>\r\n\r\n		Best regards,</br>\r\n		VetVitals Team</br>\r\n	</body>\r\n</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'), 
'zh-Hant', '建立新登入帳戶', 
'<!DOCTYPE html>\r\n<html>\r\n	<body>\r\n	  親愛的###<user_fullname>###，</br></br>\r\n\r\n	  我們很高興地通知您，您的帳戶已成功建立！\r\n\r\n	  以下是您的登入詳細資訊：</br>\r\n	  登入 ID：###<userlogin_id>###</br>\r\n	  臨時密碼：###<user_password>###</br></br>\r\n\r\n	  請不要回覆此電子郵件，因為它是自動產生的。</br></br>\r\n\r\n	  此致敬禮，</br>\r\n	  VetVitals 團隊\r\n	</body>\r\n</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'), 
'zh-Hans', '创建新登录账户', 
'<!DOCTYPE html>\r\n<html>\r\n	<body>\r\n		亲爱的 ###<user_fullname>###，</br></br>\r\n		\r\n		我们很高兴地通知您，您的帐户已成功创建！</br></br>\r\n		\r\n		以下是您的登录详细信息：</br>\r\n		登录 ID：###<userlogin_id>###</br>\r\n		临时密码：###<user_password>###</br></br>\r\n		\r\n		请不要回复此电子邮件，因为它是自动生成的。</br></br>\r\n		\r\n		此致，</br>\r\n		VetVitals 团队</br>\r\n	</body>\r\n</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'), 
'de', 'Neues Login-Konto erstellen', 
'<!DOCTYPE html>\r\n<html>\r\n	<body>\r\n		Sehr geehrter ###<user_fullname>###, </br></br>\r\n		\r\n		Wir freuen uns, Ihnen mitteilen zu können, dass Ihr Konto erfolgreich erstellt wurde!</br></br>\r\n		\r\n		Nachfolgend finden Sie Ihre Anmeldedaten:</br>\r\n		Anmelde-ID: ###<userlogin_id>###</br>\r\n		Temporäres Passwort: ###<user_password>###</br></br>\r\n		\r\n		Bitte antworten Sie nicht auf diese E-Mail, da sie automatisch generiert wird.</br></br>\r\n		\r\n		Mit freundlichen Grüßen,</br>\r\n		VetVitals-Team</br>\r\n	</body>\r\n</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'), 
'es', 'Creación de una nueva cuenta de inicio de sesión', 
'<!DOCTYPE html>\r\n<html>\r\n	<body>\r\n		Estimado ###<user_fullname>###, </br></br>\r\n		\r\n		Nos complace informarle que su cuenta se ha creado correctamente.</br></br>\r\n		\r\n		A continuación, se muestran sus datos de inicio de sesión:</br>\r\n		ID de inicio de sesión: ###<userlogin_id>###</br>\r\n		Contraseña temporal: ###<user_password>###</br></br>\r\n		\r\n		No responda a este correo electrónico, ya que se genera automáticamente.</br></br>\r\n		\r\n		Atentamente,</br>\r\n		Equipo de VetVitals</br>\r\n	</body>\r\n</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'), 
'fr', 'Création d\'\'un nouveau compte de connexion', 
'<!DOCTYPE html>\r\n<html>\r\n	<body>\r\n		Cher ###<user_fullname>###, </br></br>\r\n		\r\n		Nous avons le plaisir de vous informer que votre compte a été créé avec succès !</br></br>\r\n		\r\n		Vous trouverez ci-dessous vos identifiants de connexion :</br>\r\n		ID de connexion : ###<userlogin_id>###</br>\r\n		Mot de passe temporaire : ###<user_password>###</br></br>\r\n		\r\n		Veuillez vous abstenir de répondre à cet e-mail car il est généré automatiquement.</br></br>\r\n		\r\n		Cordialement,</br>\r\n		L\'équipe VetVitals</br>\r\n	</body>\r\n</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'), 
'hi', 'नया लॉगिन खाता निर्माण', 
'<!DOCTYPE html>\r\n<html>\r\n	<body>\r\n		प्रिय ###<user_fullname>###, </br></br>\r\n		\r\n		हमें आपको यह बताते हुए खुशी हो रही है कि आपका खाता सफलतापूर्वक बना लिया गया है!</br></br>\r\n		\r\n		नीचे आपके लॉगिन विवरण दिए गए हैं:</br>\r\n		लॉगिन आईडी: ###<userlogin_id>###</br>\r\n		अस्थायी पासवर्ड: ###<user_password>###</br></br>\r\n		\r\n		कृपया इस ईमेल का उत्तर देने से बचें क्योंकि यह स्वतः जनरेट किया गया है।</br></br>\r\n		\r\n		सादर,</br>\r\n		VetVitals टीम</br>\r\n	</body>\r\n</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'), 
'id', 'Pembuatan Akun Login Baru', 
'<!DOCTYPE html>\r\n<html>\r\n	<body>\r\n		Kepada ###<user_fullname>###, </br></br>\r\n\r\n		Dengan senang hati kami informasikan bahwa akun Anda telah berhasil dibuat!</br></br>\r\n\r\n		Berikut adalah detail login Anda:</br>\r\n		ID Login: ###<userlogin_id>###</br>\r\n		Kata Sandi Sementara: ###<user_password>###</br></br>\r\n\r\n		Harap jangan membalas email ini karena email ini dibuat secara otomatis.</br></br>\r\n\r\n		Salam,</br>\r\n		Tim VetVitals</br>\r\n	</body>\r\n</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'), 
'ja', '新しいログインアカウントの作成', 
'<!DOCTYPE html>\r\n<html>\r\n	<body>\r\n		###<user_fullname>### 様</br></br>\r\n\r\n		アカウントが正常に作成されたことをお知らせいたします。</br></br>\r\n\r\n		ログイン情報は以下の通りです:</br>\r\n		ログイン ID: ###<userlogin_id>###</br>\r\n		仮パスワード: ###<user_password>###</br></br>\r\n\r\n		このメールは自動生成されるため、返信しないでください。</br></br>\r\n\r\n		よろしくお願いいたします。</br>\r\n\r\n		VetVitals チーム</br>\r\n	</body>\r\n</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'), 
'ko', '새로운 로그인 계정 생성', 
'<!DOCTYPE html>\r\n<html>\r\n	<body>\r\n		친애하는 ###<user_fullname>###님, </br></br>\r\n		\r\n		귀하의 계정이 성공적으로 생성되었음을 알려드리게 되어 기쁩니다!</br></br>\r\n		\r\n		귀하의 로그인 정보는 다음과 같습니다.</br>\r\n		로그인 ID: ###<userlogin_id>###</br>\r\n		임시 비밀번호: ###<user_password>###</br></br>\r\n		\r\n		이 이메일은 자동 생성되므로 답장하지 마십시오.</br></br>\r\n		\r\n		감사합니다.</br>\r\n		VetVitals 팀</br>\r\n	</body>\r\n</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'), 
'pt', 'Criação de nova conta de login', 
'<!DOCTYPE html>\r\n<html>\r\n	<body>\r\n	  Caro ###<user_fullname>###, </br></br>\r\n\r\n	  Temos o prazer de informar que a sua conta foi criada com sucesso!</br></br>\r\n\r\n	  Abaixo estão os seus dados de login:</br>\r\n	  ID de login: ###<userlogin_id>###</br>\r\n	  Password temporária: ###<user_password>###</br></br>\r\n\r\n	  Por favor, evite responder a este e-mail, pois é gerado automaticamente.</br></br>\r\n\r\n	  Atenciosamente,</br>\r\n	  Equipa VetVitals\r\n	</body>\r\n</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'), 
'ru', 'Создание новой учетной записи', 
'<!DOCTYPE html>\r\n<html>\r\n	<body>\r\n		Уважаемый ###<user_fullname>###, </br></br>\r\n\r\n		Мы рады сообщить вам, что ваша учетная запись была успешно создана!</br></br>\r\n\r\n		Ниже приведены ваши данные для входа:</br>\r\n		Идентификатор входа: ###<userlogin_id>###</br>\r\n		Временный пароль: ###<user_password>###</br></br>\r\n\r\n		Пожалуйста, воздержитесь от ответа на это письмо, так как оно генерируется автоматически.</br></br>\r\n\r\n		С наилучшими пожеланиями,</br>\r\n		Команда VetVitals</br>\r\n	</body>\r\n</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'), 
'vi', 'Tạo tài khoản đăng nhập mới', 
'<!DOCTYPE html>\r\n<html>\r\n	<body>\r\n		Kính gửi ###<user_fullname>###, </br></br>\r\n\r\n		Chúng tôi vui mừng thông báo rằng tài khoản của bạn đã được tạo thành công!</br></br>\r\n\r\n		Dưới đây là thông tin chi tiết về thông tin đăng nhập của bạn:</br>\r\n		ID đăng nhập: ###<userlogin_id>###</br>\r\n		Mật khẩu tạm thời: ###<user_password>###</br></br>\r\n\r\n		Vui lòng không trả lời email này vì nó được tạo tự động.</br></br>\r\n\r\n		Trân trọng,</br>\r\n		Đội ngũ VetVitals</br>\r\n	</body>\r\n</html>', 
NOW(), 'SYSTEM');

