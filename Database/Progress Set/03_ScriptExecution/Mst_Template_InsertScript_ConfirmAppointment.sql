/*----------- Email : Appointment request confirmed --------------*/
INSERT INTO `mst_template` (`TemplateType`, `TemplateCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ('Email Notification', 'CPMS_EN003', 'Appointment request confirmed for ###<services>###', '', NOW(), 'SYSTEM');


INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN003'), 
'de', 'Terminanfrage für ###<services>### bestätigt', 
'<!DOCTYPE html>
<html>
 	<body>
		Sehr geehrter ###<customer>###, </br></br>

		Mit dieser E-Mail bestätigen wir Ihren Termin für ###<services>### am ###<appointmetdate>###, ###<appointmenttime>### für Haustier ###<petname>###. Bei Änderungen kontaktieren Sie uns bitte.</br></br>

		Bitte antworten Sie nicht auf diese E-Mail, da sie automatisch generiert wird.</br></br>

		Mit freundlichen Grüßen</br>
		VPMS-Team</br>
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN003'), 
'en', 'Appointment request confirmed for ###<services>###', 
'<!DOCTYPE html>
<html>
 	<body>
 		Dear ###<customer>###, </br></br>

 		This email confirms your ###<services>### appointment on ###<appointmetdate>###, ###<appointmenttime>### for Pet ###<petname>###. Please contact us if you need to make any changes.</br></br>

 		Please refrain from replying to this email as it is auto-generated.</br></br>

 		Best regards,</br>
 		VPMS Team</br>
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN003'), 
'es', 'Solicitud de cita confirmada para ###<services>###', 
'<!DOCTYPE html>
<html>
 	<body>
		Estimado/a ###<customer>###:</br></br>

		Este correo electrónico confirma su cita para ###<services>### el ###<appointmetdate>###, ###<appointmenttime>### para la mascota ###<petname>###. Si necesita realizar algún cambio, contáctenos.</br></br>

		No responda a este correo electrónico, ya que se genera automáticamente.</br></br>

		Atentamente,</br>

		Equipo VPMS
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN003'), 
'fr', 'Demande de rendez-vous confirmée pour ###<services>###', 
'<!DOCTYPE html>
<html>
 	<body>
		Cher ###<customer>###, </br></br>

		Cet e-mail confirme votre rendez-vous ###<services>### le ###<appointmetdate>###, ###<appointmenttime>### pour votre animal ###<petname>###. Veuillez nous contacter si vous souhaitez apporter des modifications.</br></br>

		Veuillez ne pas répondre à cet e-mail, car il est généré automatiquement.</br></br>

		Cordialement,</br>
		Équipe VPMS</br>
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN003'), 
'hi', '###<services>### के लिए नियुक्ति अनुरोध की पुष्टि की गई', 
'<!DOCTYPE html>
<html>
 	<body>
		प्रिय ###<customer>###, </br></br>

		यह ईमेल आपके ###<services>### अपॉइंटमेंट की पुष्टि करता है ###<appointmetdate>###, ###<appointmenttime>### पालतू जानवर ###<petname>### के लिए। यदि आपको कोई बदलाव करने की आवश्यकता हो तो कृपया हमसे संपर्क करें।</br></br>

		कृपया इस ईमेल का उत्तर देने से बचें क्योंकि यह स्वतः जनरेट किया गया है।</br></br>

		सादर,</br>
		VPMS टीम</br>
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN003'), 
'id', 'Permintaan janji temu dikonfirmasi untuk ###<services>###', 
'<!DOCTYPE html>
<html>
 	<body>
		Kepada ###<customer>### yang terhormat, </br></br>

		Email ini mengonfirmasi janji temu ###<services>### Anda pada ###<appointmetdate>###, ###<appointmenttime>### untuk Hewan Peliharaan ###<petname>###. Silakan hubungi kami jika Anda perlu melakukan perubahan apa pun.</br></br>

		Harap jangan membalas email ini karena email ini dibuat secara otomatis.</br></br>

		Salam,</br>
		Tim VPMS</br>
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN003'), 
'ja', '###<services>### の予約リクエストが確認されました', 
'<!DOCTYPE html>
<html>
 	<body>
		###<customer>### 様</br></br>

		このメールは、###<services>### のご予約（###<appointmetdate>###、###<appointmenttime>###）の確認となります。ご予約内容に変更がある場合は、ご連絡ください。</br></br>

		このメールは自動生成されているため、ご返信はご遠慮ください。</br></br>

		よろしくお願いいたします。</br>
		VPMS チーム</br>
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN003'), 
'ko', '###<services>###에 대한 예약 요청이 확인되었습니다.', 
'<!DOCTYPE html>
<html>
 	<body>
		###<customer>###님,</br></br>

		본 이메일은 고객님의 ###<services>### 예약(###<appointmetdate>###, ###<appointmenttime>###)을 확인드립니다. 예약 변경 사항이 있으시면 언제든지 문의해 주세요.</br></br>

		본 이메일은 자동 생성되므로 회신하지 말아 주세요.</br></br>

		감사합니다.</br>
		VPMS 팀</br>
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN003'), 
'pt', 'Pedido de agendamento confirmado para ###<services>###', 
'<!DOCTYPE html>
<html>
 	<body>
		Caro ###<customer>###, </br></br>

		Este e-mail confirma o seu agendamento ###<services>### em ###<appointmetdate>###, ###<appointmenttime>### para o Pet ###<petname>###. Contacte-nos se precisar de fazer alguma alteração.</br></br>

		Por favor, evite responder a este e-mail, pois é gerado automaticamente.</br></br>

		Atenciosamente,
		Equipa VPMS
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN003'), 
'ru', 'Запрос на прием подтвержден для ###<services>###', 
'<!DOCTYPE html>
<html>
 	<body>
		Уважаемый ###<customer>###, </br></br>

		Это письмо подтверждает вашу ###<services>### запись на ###<appointmetdate>###, ###<appointmenttime>### для питомца ###<petname>###. Пожалуйста, свяжитесь с нами, если вам нужно внести какие-либо изменения.</br></br>

		Пожалуйста, воздержитесь от ответа на это письмо, так как оно создается автоматически.</br></br>

		С наилучшими пожеланиями,</br>
		Команда VPMS</br>
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN003'), 
'vi', 'Yêu cầu đặt lịch hẹn đã được xác nhận cho ###<services>###', 
'<!DOCTYPE html>
<html>
 	<body>
		Kính gửi ###<customer>###, </br></br>

		Email này xác nhận cuộc hẹn ###<services>### của bạn vào ngày ###<appointmetdate>###, ###<appointmenttime>### cho Thú cưng ###<petname>###. Vui lòng liên hệ với chúng tôi nếu bạn cần thực hiện bất kỳ thay đổi nào.</br></br>

		Vui lòng không trả lời email này vì nó được tạo tự động.</br></br>

		Trân trọng,</br>
		Nhóm VPMS</br>
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN003'), 
'zh-Hans', '已确认 ###<services>### 的预约请求', 
'<!DOCTYPE html>
<html>
 	<body>
		尊敬的###<customer>###，</br></br>

		此邮件确认您已于###<appointmetdate>###、###<appointmenttime>###预约###<services>###宠物###<petname>###。如有任何更改，请联系我们。</br></br>

		此邮件为自动生成，请勿回复。</br></br>

		此致，</br>
		VPMS团队</br>
 	</body>
</html>', 
NOW(), 'SYSTEM');


INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN003'), 
'zh-Hant', '已確認 ###<services>### 的預約請求', 
'<!DOCTYPE html>
<html>
 	<body>
		親愛的###<customer>###，</br></br>

		此電子郵件確認您於 ###<appointmetdate>###、###<appointmenttime>### 為寵物 ###<petname>### 預約的 ###<services>###。如果您需要進行任何更改，請聯絡我們。 </br></br>

		請不要回覆此電子郵件，因為它是自動產生的。 </br></br>

		此致敬禮，</br>
		VPMS團隊
 	</body>
</html>', 
NOW(), 'SYSTEM');