/*----------- Email : Customer Portal Account Creation --------------*/
INSERT INTO mst_template(TemplateType, TemplateCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES('Email Notification', 'CPMS_EN001', 'Vet Vitals Customer Portal activation', '', 
NOW(), 'SYSTEM');


INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN001'),
'de', 'Aktivierung des Vet Vitals-Kundenportals',
'<!DOCTYPE html>
<html>
 	<body>
		Sehr geehrte(r) ###<Kunde>###, </br></br>

		Nur einen Klick trennen Sie vom Vet Vitals Kundenportal. Klicken Sie auf die Schaltfläche unten, um die Kontoaktivierung zu starten.</br></br>

		<a href="###<creationlink>###">
		<input type="button" value="Konto aktivieren" />
		</a>
		<br/><br/>
		Bitte antworten Sie nicht auf diese E-Mail, da sie automatisch generiert wird.</br></br>

		Mit freundlichen Grüßen</br>
		VPMS-Team</br>
 	</body>
</html>',
NOW(), 'SYSTEM');


INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN001'),
'en', 'Vet Vitals Customer Portal activation',
'<!DOCTYPE html>
<html>
 	<body>
 		Dear ###<customer>###, </br></br>

 		You''re just one click away from getting started with Vet Vitals Customer portal. Click on the button below to initiate activate account process.</br></br>

		<a href="###<creationlink>###">
 		<input type="button" value="Activate Account" /> 
		</a>
		<br/><br/>
 		Please refrain from replying to this email as it is auto-generated.</br></br>

 		Best regards,</br>
 		VPMS Team</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN001'),
'es', 'Activación del portal de clientes de Vet Vitals',
'<!DOCTYPE html>
<html>
 	<body>
		Estimado/a ###<customer>###:<br/><br/>

		Está a solo un clic de comenzar a usar el portal de clientes de Vet Vitals. Haga clic en el botón de abajo para iniciar el proceso de activación de su cuenta.<br/><br/>

		<a href="###<creationlink>###">
		<input type="button" value="Activate Account" />
		</a>
		<br/><br/>
		No responda a este correo electrónico, ya que se genera automáticamente.<br/><br/>

		Atentamente,<br/><br/>

		Equipo de VPMS<br/>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN001'),
'fr', 'Activation du portail client Vet Vitals',
'<!DOCTYPE html>
<html>
 	<body>
		Cher ###<client>###, </br></br>

		Vous n''êtes plus qu''à un clic de démarrer sur le portail client Vet Vitals. Cliquez sur le bouton ci-dessous pour lancer le processus d''activation de votre compte.</br></br>

		<a href="###<creationlink>###">
		<input type="button" value="Activer le compte" />
		</a>
		<br/><br/>
		Veuillez ne pas répondre à cet e-mail, car il est généré automatiquement.</br></br>

		Cordialement,</br><br/>
		L''équipe VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN001'),
'hi', 'वेट विटल्स ग्राहक पोर्टल सक्रियण',
'<!DOCTYPE html>
<html>
 	<body>
		प्रिय ###<customer>###, </br></br>

		आप Vet Vitals ग्राहक पोर्टल के साथ आरंभ करने से बस एक क्लिक दूर हैं। खाता सक्रिय करने की प्रक्रिया आरंभ करने के लिए नीचे दिए गए बटन पर क्लिक करें।</br></br>

		<a href="###<creationlink>###">
		<input type="button" value="खाता सक्रिय करें" />
		</a>
		<br/><br/>
		कृपया इस ईमेल का उत्तर देने से बचें क्योंकि यह स्वतः जनरेट किया गया है।</br></br>

		सादर,</br><br/>
		VPMS टीम</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN001'),
'id', 'Aktivasi Portal Pelanggan Vet Vitals',
'<!DOCTYPE html>
<html>
 	<body>
		Kepada ###<customer>###, </br></br>

		Anda tinggal klik satu kali untuk memulai portal Pelanggan Vet Vitals. Klik tombol di bawah ini untuk memulai proses aktivasi akun.</br></br>

		<a href="###<creationlink>###">
		<input type="button" value="Aktifkan Akun" />
		</a>
		<br/><br/>
		Harap jangan membalas email ini karena email ini dibuat secara otomatis.</br></br>

		Salam,</br><br/>
		Tim VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN001'),
'ja', 'Vet Vitals カスタマー ポータルの有効化',
'<!DOCTYPE html>
<html>
 	<body>
		###<customer>### 様、</br></br>

		Vet Vitals カスタマー ポータルの利用を開始するには、あと 1 回のクリックで済みます。下のボタンをクリックして、アカウントの有効化プロセスを開始してください。</br></br>

		<a href="###<creationlink>###">
		<input type="button" value="Activate Account" />
		</a>
		<br/><br/>
		このメールは自動生成されるため、返信しないでください。</br></br>

		よろしくお願いいたします。</br><br/>

		VPMS チーム</br>
 	</body>
</html>',
NOW(), 'SYSTEM');


INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN001'),
'ko', 'Vet Vitals 고객 포털 활성화',
'<!DOCTYPE html>
<html>
 	<body>
		친애하는 ###<customer>###님, </br></br>

		Vet Vitals 고객 포털을 시작하기 위해 클릭 한 번만 남았습니다. 아래 버튼을 클릭하여 계정 활성화 프로세스를 시작하세요.</br></br>

		<a href="###<creationlink>###">
		<input type="button" value="Activate Account" />
		</a>
		<br/><br/>
		이 이메일은 자동 생성되므로 답장하지 마세요.</br></br>

		감사합니다.</br><br/>
		VPMS 팀</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN001'),
'pt', 'Ativação do Portal do Cliente Vet Vitals',
'<!DOCTYPE html>
<html>
 	<body>
		 Caro ###<cliente>###, </br></br>
e
		 Está a apenas um clique de começar a utilizar o portal de clientes Vet Vitals. Clique no botão abaixo para iniciar o processo de ativação da conta.</br></br>

		 <a href="###<link de criação>###">		Уважаемый ###<customer>###, </br></br>

		Вы всего в одном клике от начала работы с порталом клиентов Vet Vitals. Нажмите кнопку ниже, чтобы начать процесс активации учетной записи.</br></br>

		<a href="###<creationlink>###">
		<input type="button" value="Активировать учетную запись" />
		</a>
		<br/><br/>
		Пожалуйста, воздержитесь от ответа на это письмо, так как оно создается автоматически.</br></br>

		С наилучшими пожеланиями,</br><br/>
		Команда VPMS</br>
		 <input type="button" value="Ativar conta" />
		 </a>
		 <br/><br/>
		 Por favor, evite responder a este e-mail, pois é gerado automaticamente.</br></br>

		 Atenciosamente,</br><br />
		 Equipa VPMS
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN001'),
'ru', 'Активирање на порталот за клиенти на Vet Vitals',
'<!DOCTYPE html>
<html>
 	<body>
		Уважаемый ###<customer>###, </br></br>

		Вы всего в одном клике от начала работы с порталом клиентов Vet Vitals. Нажмите кнопку ниже, чтобы начать процесс активации учетной записи.</br></br>

		<a href="###<creationlink>###">
		<input type="button" value="Активировать учетную запись" />
		</a>
		<br/><br/>
		Пожалуйста, воздержитесь от ответа на это письмо, так как оно создается автоматически.</br></br>

		С наилучшими пожеланиями,</br><br/>
		Команда VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN001'),
'vi', 'Kích hoạt Cổng thông tin khách hàng Vet Vitals',
'<!DOCTYPE html>
<html>
 	<body>
		Kính gửi ###<customer>###, </br></br>

		Bạn chỉ cần nhấp một lần là có thể bắt đầu sử dụng cổng thông tin khách hàng của Vet Vitals. Nhấp vào nút bên dưới để bắt đầu quy trình kích hoạt tài khoản.</br></br>

		<a href="###<creationlink>###">
		<input type="button" value="Activate Account" />
		</a>
		<br/><br/>
		Vui lòng không trả lời email này vì nó được tạo tự động.</br></br>

		Trân trọng,</br><br/>
		Nhóm VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');


INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN001'),
'zh-Hans', 'Vet Vitals 客户门户激活',
'<!DOCTYPE html>
<html>
 	<body>
		尊敬的 ###<customer>###，</br></br>

		只需单击一下即可开始使用 Vet Vitals 客户门户。单击下面的按钮启动激活帐户流程。</br></br>

		<a href="###<creationlink>###">
		<input type="button" value="激活帐户" />
		</a>
		<br/><br/>
		请不要回复此电子邮件，因为它是自动生成的。</br></br>

		此致，</br><br/>
		VPMS 团队</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN001'),
'zh-Hant', 'Vet Vitals 客戶入口網站激活',
'<!DOCTYPE html>
<html>
 	<body>
		親愛的###<customer>###，</br></br>

		只需單擊一次即可開始使用 Vet Vitals 客戶入口網站。點擊下面的按鈕即可啟動啟動帳戶流程。 </br></br>

		<a href="###<creationlink>###">
		<input type="button" value="啟動帳號" />
		</a>
		<br/><br/>
		請不要回覆此電子郵件，因為它是自動產生的。 </br></br>

		此致敬禮，</br><br/>
		VPMS 團隊
 	</body>
</html>',
NOW(), 'SYSTEM');