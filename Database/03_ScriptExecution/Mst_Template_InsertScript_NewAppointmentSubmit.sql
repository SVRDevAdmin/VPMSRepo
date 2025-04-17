/*----------- Email : New Appointment request --------------*/
INSERT INTO `mst_template` (`TemplateType`, `TemplateCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ('Email Notification', 'CPMS_EN002', 'New Appointment request for ###<services>###', '', NOW(), 'SYSTEM');


INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN002'), 
'de', 'Neue Terminanfrage für ###<services>###', 
'<!DOCTYPE html>
<html>
 	<body>
		Sehr geehrter ###<customer>###, </br></br>

		Vielen Dank für Ihre Terminanfrage für ###<services>###. Wir bearbeiten Ihre Anfrage derzeit und werden sie in Kürze bestätigen. Sie erhalten anschließend eine weitere E-Mail mit einer Bestätigung oder der Bitte um weitere Informationen.</br></br>

		Termindetails:</br></br>
		Datum: ###<appointmetdate>###</br>
		Uhrzeit: ###<appointmenttime>###</br>
		Name des Tieres: ###<petname>###</br>
		Leistungen: ###<services>###</br>
		Arzt: ###<doctorname>###</br></br>

		Bitte antworten Sie nicht auf diese E-Mail, da sie automatisch generiert wird.</br></br>

		Mit freundlichen Grüßen</br>
		VPMS-Team</br>
 	</body>
</html>', 
NOW(), 'SYSTEM');


INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN002'), 
'en', 'New Appointment request for ###<services>###', 
'<!DOCTYPE html>
<html>
 	<body>
 		Dear ###<customer>###, </br></br>

 		Thank you for your recent appointment request for ###<services>###. We are currently processing your request and will confirm it status shortly. You will receive another email with either confirmation or a requesr for more information.</br></br>

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

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN002'), 
'es', 'Nueva solicitud de cita para ###<services>###', 
'<!DOCTYPE html>
<html>
 	<body>
		Estimado/a ###<customer>###: </br></br>

		Gracias por su reciente solicitud de cita para ###<services>###. Estamos procesando su solicitud y confirmaremos su estado en breve. Recibirá otro correo electrónico con la confirmación o una solicitud de más información.</br></br>

		A continuación, se muestran los detalles de la cita:</br></br>
		Fecha: ###<appointmetdate>###</br>
		Hora: ###<appointmenttime>###</br>
		Nombre de la mascota: ###<petname>###</br>
		Servicios: ###<services>###</br>
		Médico: ###<doctorname>###</br></br>

		No responda a este correo electrónico, ya que se genera automáticamente.</br></br>

		Atentamente,</br>

		Equipo VPMS
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN002'), 
'fr', 'Nouvelle demande de rendez-vous pour ###<services>###', 
'<!DOCTYPE html>
<html>
 	<body>
		Cher ###<customer>###, </br></br>

		Nous vous remercions pour votre récente demande de rendez-vous pour ###<services>###. Nous traitons actuellement votre demande et vous confirmerons son statut prochainement. Vous recevrez un autre e-mail de confirmation ou de demande d''informations complémentaires.</br></br>

		Voici les détails du rendez-vous :</br></br>
		Date : ###<appointmetdate>###</br>
		Heure : ###<appointmenttime>###</br>
		Nom de l''animal : ###<petname>###</br>
		Services : ###<services>###</br>
		Médecin : ###<doctorname>###</br></br>

		Veuillez ne pas répondre à cet e-mail, il est généré automatiquement.</br></br>

		Cordialement,</br>
		Équipe VPMS</br>
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN002'), 
'hi', '###<services>### के लिए नई नियुक्ति का अनुरोध', 
'<!DOCTYPE html>
<html>
 	<body>
		प्रिय ###<customer>###, </br></br>

		###<services>### के लिए आपके हाल ही के अपॉइंटमेंट अनुरोध के लिए धन्यवाद। हम वर्तमान में आपके अनुरोध पर कार्रवाई कर रहे हैं और जल्द ही इसकी स्थिति की पुष्टि करेंगे। आपको पुष्टि या अधिक जानकारी के लिए अनुरोध के साथ एक और ईमेल प्राप्त होगा।</br></br>

		नीचे अपॉइंटमेंट विवरण दिए गए हैं:</br></br>
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

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN002'), 
'id', 'Permintaan Janji Temu Baru untuk ###<services>###', 
'<!DOCTYPE html>
<html>
 	<body>
		Kepada ###<customer>### yang terhormat, </br></br>

		Terima kasih atas permintaan janji temu terbaru Anda untuk ###<services>###. Saat ini kami sedang memproses permintaan Anda dan akan segera mengonfirmasi statusnya. Anda akan menerima email lain dengan konfirmasi atau permintaan informasi lebih lanjut.</br></br>

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

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN002'), 
'ja', '###<services>### の新しい予約リクエスト', 
'<!DOCTYPE html>
<html>
 	<body>
		###<customer>### 様</br></br>

		###<services>### のご予約をいただき、誠にありがとうございます。現在、ご予約を処理中で、近日中に状況を確認いたします。確認メールまたは詳細のお問い合わせフォームを改めてお送りいたします。</br></br>

		ご予約の詳細は以下の通りです。</br></br>
		日付: ###<appointmetdate>###</br>
		時間: ###<appointmenttime>###</br>
		ペット名: ###<petname>###</br>
		サービス: ###<services>###</br>
		医師: ###<doctorname>###</br></br>

		このメールは自動生成されているため、返信はご遠慮ください。</br></br>

		よろしくお願いいたします。</br>
		VPMS チーム</br>
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN002'), 
'ko', '###<services>###에 대한 새로운 약속 요청', 
'<!DOCTYPE html>
<html>
	<body>
		###<customer>###님, 안녕하세요.</br></br>

		###<services>###에 대한 최근 예약 요청을 보내주셔서 감사합니다. 현재 요청을 처리 중이며 곧 처리 상태를 확인해 드리겠습니다. 확인 이메일 또는 추가 정보 요청 이메일을 다시 보내드리겠습니다.</br></br>

		예약 세부 정보는 다음과 같습니다.</br></br>
		날짜: ###<appointmetdate>###</br>
		시간: ###<appointmenttime>###</br>
		반려동물 이름: ###<petname>###</br>
		서비스: ###<services>###</br>
		담당 의사: ###<doctorname>###</br></br>

		본 이메일은 자동 생성되므로 회신하지 말아 주세요.</br></br>

		감사합니다.</br>
		VPMS 팀</br>
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN002'), 
'pt', 'Novo pedido de agendamento para ###<services>###', 
'<!DOCTYPE html>
<html>
	<body>
		Caro ###<customer>###, </br></br>

		Obrigado pela sua recente solicitação de agendamento para ###<services>###. Estamos a processar a sua solicitação e confirmaremos o seu estado em breve. Receberá outro e-mail com confirmação ou pedido de mais informações.</br></br>

		Abaixo seguem os detalhes do agendamento:</br></br>
		Data: ###<appointmenttime>###</br>
		Hora: ###<appointmenttime>###</br>
		Nome do animal de estimação: ###<petname>###</br>
		Serviços: ###<services>###</br>
		Médico: ###<doctorname>###</br></br>

		Por favor, evite responder a este e-mail, pois é gerado automaticamente.</br></br>

		Atenciosamente,
		Equipa VPMS
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN002'), 
'ru', 'Новый запрос на прием для ###<services>###', 
'<!DOCTYPE html>
<html>
	<body>
		Уважаемый ###<customer>###, </br></br>

		Благодарим вас за недавний запрос на прием для ###<services>###. В настоящее время мы обрабатываем ваш запрос и вскоре подтвердим его статус. Вы получите еще одно письмо с подтверждением или запросом на дополнительную информацию.</br></br>

		Ниже приведены сведения о приеме:</br></br>
		Дата: ###<appointmetdate>###</br>
		Время: ###<appointmenttime>###</br>
		Кличка питомца: ###<petname>###</br>
		Услуги: ###<services>###</br>
		Врач: ###<doctorname>###</br></br>

		Пожалуйста, воздержитесь от ответа на это письмо, так как оно создается автоматически.</br></br>

		С наилучшими пожеланиями,</br>
		Команда VPMS</br>
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN002'), 
'ru', 'Новый запрос на прием для ###<services>###', 
'<!DOCTYPE html>
<html>
	<body>
		Уважаемый ###<customer>###, </br></br>

		Благодарим вас за недавний запрос на прием для ###<services>###. В настоящее время мы обрабатываем ваш запрос и вскоре подтвердим его статус. Вы получите еще одно письмо с подтверждением или запросом на дополнительную информацию.</br></br>

		Ниже приведены сведения о приеме:</br></br>
		Дата: ###<appointmetdate>###</br>
		Время: ###<appointmenttime>###</br>
		Кличка питомца: ###<petname>###</br>
		Услуги: ###<services>###</br>
		Врач: ###<doctorname>###</br></br>

		Пожалуйста, воздержитесь от ответа на это письмо, так как оно создается автоматически.</br></br>

		С наилучшими пожеланиями,</br>
		Команда VPMS</br>
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN002'), 
'vi', 'Yêu cầu cuộc hẹn mới cho ###<services>###', 
'<!DOCTYPE html>
<html>
	<body>
		Kính gửi ###<customer>###, </br></br>

		Cảm ơn bạn đã yêu cầu đặt lịch hẹn gần đây cho ###<services>###. Chúng tôi hiện đang xử lý yêu cầu của bạn và sẽ sớm xác nhận trạng thái. Bạn sẽ nhận được một email khác có xác nhận hoặc yêu cầu thêm thông tin.</br></br>

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

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN002'), 
'zh-Hans', '###<services>### 的新预约请求', 
'<!DOCTYPE html>
<html>
	<body>
		尊敬的###<customer>###，</br></br>

		感谢您最近对###<services>###的预约请求。我们目前正在处理您的请求，并将很快确认状态。您将收到另一封电子邮件，其中包含确认信息或更多信息请求。</br></br>

		以下是预约详情：</br></br>
		日期：###<appointmetdate>###</br>
		时间：###<appointmenttime>###</br>
		宠物姓名：###<petname>###</br>
		服务：###<services>###</br>
		医生：###<doctorname>###</br></br>

		此邮件为自动生成，请勿回复。</br></br>

		此致，</br>
		VPMS团队</br>
 	</body>
</html>', 
NOW(), 'SYSTEM');

INSERT INTO `mst_template_details` (`TemplateID`, `LangCode`, `TemplateTitle`, `TemplateContent`, `CreatedDate`, `CreatedBy`) 
VALUES ((SELECT TemplateID FROM mst_template WHERE TemplateCode='CPMS_EN002'), 
'zh-Hant', '###<services>### 的新預約請求', 
'<!DOCTYPE html>
<html>
	<body>
		親愛的###<customer>###，</br></br>

		感謝您最近對###<services>###的預約請求。我們目前正在處理您的請求並將很快確認其狀態。您將收到另一封電子郵件，其中包含確認訊息或更多資訊請求。 </br></br>

		以下是預約詳情：</br></br>
		日期：###<appointmetdate>###</br>
		時間：###<appointmenttime>###</br>
		寵物名稱：###<petname>###</br>
		服務：###<services>###</br>
		醫生：###<doctorname>###</br></br>

		請不要回覆此電子郵件，因為它是自動產生的。 </br></br>

		此致敬禮，</br>
		VPMS團隊
 	</body>
</html>', 
NOW(), 'SYSTEM');