INSERT INTO `mst_template`
(`TemplateType`,`TemplateCode`,`TemplateTitle`,`TemplateContent`,`CreatedDate`,`CreatedBy`)
VALUES
('Email Notification', 'VPMS_EN001', 'New Appointment Creation', '', NOW(), 'SYSTEM'),
('Password Reset & Recovery Email','VPMS_EN002','','',now(),'System');

-- VPMS_EN001
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

-- VPMS_EN002
INSERT INTO `mst_template_details`
(`TemplateID`,`LangCode`,`TemplateTitle`,`TemplateContent`,`CreatedDate`,`CreatedBy`)
VALUES
((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN002'),'en','[VPMS] Password Reset & Recovery',
'
 <!DOCTYPE html>
 <html>
 	<body>
 		Dear ###<staff_fullname>###, </br></br>
 
 		This is your temporary password ###<password>###.</br></br>
 
 		Please utilize this temporary password to regain access to your account.</br></br>
 		
 		Please refrain from replying to this email as it is auto-generated.</br></br>
 
 		Best regards,</br>
 		VPMS Team</br>
 	</body>
 </html>
 '
,now(),'System'),
((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN002'),'zh-Hans','[VPMS] 密码重置和恢复',
'
 <!DOCTYPE html>
 <html>
 	<body>
 		亲爱的 ###<staff_fullname>###，</br></br>
 
 		这是您的临时密码###<password>###。</br></br>
 
 		请使用此临时密码重新访问您的帐户。</br></br>
 		
 		请不要回复此电子邮件，因为它是自动生成的。</br></br>
 
 		谨致问候，</br>
 		VPMS团队
 	</body>
 </html>
 '
,now(),'System'),
((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN002'),'zh-Hant','[VPMS] 密碼重置與恢復',
'
 <!DOCTYPE html>
 <html>
 	<body>
 		親愛的###<staff_fullname>###，</br></br>
 
 		這是您的臨時密碼###<password>###。
 
 		請使用此臨時密碼重新造訪您的帳戶。
 		
 		請不要回覆此電子郵件，因為它是自動產生的。
 
 		謹致問候，</br>
 		VPMS團隊</br>
 	</body>
 </html>
 '
,now(),'System'),
((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN002'),'ko','[VPMS] 비밀번호 재설정 및 복구',
'
 <!DOCTYPE html>
 <html>
 	<body>
 		###<staff_fullname>###님, </br></br>님께
 
 		임시 비밀번호 ###<password>###입니다.</br></br>
 
 		귀하의 계정에 다시 액세스하려면 이 임시 비밀번호를 활용하십시오.</br></br>
 		
 		본 이메일은 자동으로 생성된 이메일이므로 회신을 삼가해 주시기 바랍니다.</br></br>
 
 		감사합니다.</br>
 		VPMS 팀</br>
 	</body>
 </html>
 '
,now(),'System');

-- VPMS_EN003
INSERT INTO mst_template(TemplateType, TemplateCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES('Invoice Email Notification', 'VPMS_EN003', 'Invoice Creation', '', 
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES
((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'en', 'Invoice Creation',
'
<style>    
	.modal-content-custom {
		border: 0.1vw solid !important;
		padding: 1vw;
	}
    
    .firstColumn {
		width: 20vw !important;
	}

	.secondColumn {
		width: 14vw !important;
	}

	.thirdColumn {
		width: 10vw !important;
	}
    
    table{
		border-collapse: collapse;
	}

	td.borderBelow {
		border-style: solid;
		border-width: 0;
		border-bottom-width: 0.1vw;
	}

	td.borderUpBottom {
		border-style: solid;
		border-width: 0;
		border-bottom-width: 0.1vw;
		border-top-width: 0.1vw;
	}

	.modalHeader{
		padding: 0 1vw !important;
		border-bottom: 1px solid #dee2e6;
		margin-bottom: 1vw;
	}

	.addressContainer{
		width: 19vw !important;
	}
    
    .modal-content-custom {
		border: 0.1vw solid !important;
		padding: 1vw;
	}
</style>

  <div class="modal-dialog" style="margin: 1vw auto; width: fit-content;">
		<div class="modal-content modal-content-custom">
			<div class="modal-header modalHeader" style="display: flex; justify-content: space-between; align-items: center;">
				<div style="font-weight: bold;" class="printFont invoiceReceiptNo">###<invoiceReceiptNoLabel>###: ###<invoiceReceiptNo>###</div>
			</div>

			<div class="modal-body" style="display: flex;">
				<div style="margin: 0 1vw;">
					<div>
						<span style="font-size: 1vw;" class="printFont ownerName">###<ownerName>###</span>
					</div>

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">Contact No</span>
						<span class="printFont ownerNo" style="color: black; font-size: 1vw;">###<contactNo>###</span>
					</div>

					<div class="addressContainer" style="display: flex; flex-direction: column; margin: 1vw 0; width: 15vw;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">Address</span>
						<span class="printFont ownerAddress" style="color: black; font-size: 1vw;">###<address>### </span>
					</div>

					<hr style="border: 0.1vw solid black;">

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">Pet Name</span>
						<span class="printFont petName" style="color: black; font-size: 1vw;">###<petName>###</span>
					</div>

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">Registration No.</span>
						<span class="printFont petRegistrationNo" style="color: black; font-size: 1vw;">###<registrationNo>###</span>
					</div>

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">Species</span>
						<span class="printFont petSpecies" style="color: black; font-size: 1vw;">###<species>###</span>
					</div>

				</div>
				

				<div>
					<div>
						<span style="font-size: 1vw; font-weight: bold;" class="printFont">Service Name : </span><span class="printFont serviceName" style="font-size: 1vw;">###<serviceName>###</span>
					</div>
					<div style="margin-bottom: 1vw;">
						<span style="font-size: 1vw; font-weight: bold;" class="printFont">Date : </span><span class="printFont serviceDate" style="font-size: 1vw;">###<serviceDate>###</span>
					</div>

					<div style="margin-bottom: 1vw; padding: 1vw; background-color: whitesmoke;">
						<table class="printFont" id="serviceList" style="font-size: 1vw;">
                        <tbody>
                        <tr style="border-bottom-width: 0.1vw;">
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">Service</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">Discount (%)</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">Price ($)</td>
                        </tr> 
                        </tbody>
                        ###<serviceList>###
                        </table>
					</div>

					<div style="padding: 1vw; background-color: whitesmoke;">
						<table class="printFont" id="productList" style="font-size: 1vw;">
                        <tbody>
                        <tr style="border-bottom-width: 0.1vw;">
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">Product</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">Discount (%)</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">Price ($)</td>
                        </tr> 
                        </tbody>
                        ###<productList>###
                        </table>
					</div>

					<div style="padding: 1vw;">
						<table class="printFont" style="font-size: 1vw;">
							<tbody><tr>
								<td style="padding: 0 1vw;" class="firstColumn">Total</td>
								<td style="padding: 0 1vw;" class="secondColumn"></td>
								<td style="padding: 0 1vw;" class="thirdColumn total">###<total>###</td>
							</tr>
							<tr>
								<td style="padding: 0 1vw;" class="firstColumn">Tax (6%)</td>
								<td style="padding: 0 1vw;" class="secondColumn"></td>
								<td style="padding: 0 1vw;" class="thirdColumn tax">###<tax>###</td>
							</tr>
							<tr>
								<td style="padding: 0 1vw;" class="firstColumn">Grand Discount %</td>
								<td style="padding: 0 1vw;" class="secondColumn"></td>
								<td style="padding: 0 1vw;" class="thirdColumn grandDiscount">###<grandDiscount>###</td>
							</tr>
							<tr style="border-top-width: 0.2vw; border-bottom-width: 0.2vw; border-color: black;">
								<td style="padding: 0 1vw; font-weight: bold;" class="firstColumn borderUpBottom">Grand Total</td>
								<td style="padding: 0 1vw;" class="secondColumn borderUpBottom"></td>
								<td style="padding: 0 1vw; font-weight: bold;" class="thirdColumn grandTotal borderUpBottom">###<grandTotal>###</td>
							</tr>
						</tbody></table>
					</div>

					<div>
						<span style="font-size: 1vw;" class="printFont">Next Appointment : </span><span class="printFont nextAppointmentDate" style="font-size: 1vw; font-weight: bold;">###<nextApptDate>###</span>
					</div>
					<div>
						<span style="font-size: 1vw;" class="printFont">Surgery/Vaccination/Consultation Name : </span><span class="printFont nextAppointmentName" style="font-size: 1vw; font-weight: bold;">###<nextApptName>###</span>
					</div>

				</div>
			</div>
		</div>
	</div>
',
NOW(), 'SYSTEM'),
((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'ko', '송장 생성',
'
<style>    
	.modal-content-custom {
		border: 0.1vw solid !important;
		padding: 1vw;
	}
    
    .firstColumn {
		width: 20vw !important;
	}

	.secondColumn {
		width: 14vw !important;
	}

	.thirdColumn {
		width: 10vw !important;
	}
    
    table{
		border-collapse: collapse;
	}

	td.borderBelow {
		border-style: solid;
		border-width: 0;
		border-bottom-width: 0.1vw;
	}

	td.borderUpBottom {
		border-style: solid;
		border-width: 0;
		border-bottom-width: 0.1vw;
		border-top-width: 0.1vw;
	}

	.modalHeader{
		padding: 0 1vw !important;
		border-bottom: 1px solid #dee2e6;
		margin-bottom: 1vw;
	}

	.addressContainer{
		width: 19vw !important;
	}
    
    .modal-content-custom {
		border: 0.1vw solid !important;
		padding: 1vw;
	}
</style>

  <div class="modal-dialog" style="margin: 1vw auto; width: fit-content;">
		<div class="modal-content modal-content-custom">
			<div class="modal-header modalHeader" style="display: flex; justify-content: space-between; align-items: center;">
				<div style="font-weight: bold;" class="printFont invoiceReceiptNo">###<invoiceReceiptNoLabel>###: ###<invoiceReceiptNo>###</div>
			</div>

			<div class="modal-body" style="display: flex;">
				<div style="margin: 0 1vw;">
					<div>
						<span style="font-size: 1vw;" class="printFont ownerName">###<ownerName>###</span>
					</div>

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">연락처 번호</span>
						<span class="printFont ownerNo" style="color: black; font-size: 1vw;">###<contactNo>###</span>
					</div>

					<div class="addressContainer" style="display: flex; flex-direction: column; margin: 1vw 0; width: 15vw;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">주소</span>
						<span class="printFont ownerAddress" style="color: black; font-size: 1vw;">###<address>### </span>
					</div>

					<hr style="border: 0.1vw solid black;">

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">애칭</span>
						<span class="printFont petName" style="color: black; font-size: 1vw;">###<petName>###</span>
					</div>

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">등록번호</span>
						<span class="printFont petRegistrationNo" style="color: black; font-size: 1vw;">###<registrationNo>###</span>
					</div>

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">종</span>
						<span class="printFont petSpecies" style="color: black; font-size: 1vw;">###<species>###</span>
					</div>

				</div>
				

				<div>
					<div>
						<span style="font-size: 1vw; font-weight: bold;" class="printFont">서비스 이름 : </span><span class="printFont serviceName" style="font-size: 1vw;">###<serviceName>###</span>
					</div>
					<div style="margin-bottom: 1vw;">
						<span style="font-size: 1vw; font-weight: bold;" class="printFont">날짜 : </span><span class="printFont serviceDate" style="font-size: 1vw;">###<serviceDate>###</span>
					</div>

					<div style="margin-bottom: 1vw; padding: 1vw; background-color: whitesmoke;">
						<table class="printFont" id="serviceList" style="font-size: 1vw;">
                        <tbody>
                        <tr style="border-bottom-width: 0.1vw;">
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">서비스</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">할인 (%)</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">값 ($)</td>
                        </tr> 
                        </tbody>
                        ###<serviceList>###
                        </table>
					</div>

					<div style="padding: 1vw; background-color: whitesmoke;">
						<table class="printFont" id="productList" style="font-size: 1vw;">
                        <tbody>
                        <tr style="border-bottom-width: 0.1vw;">
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">제품</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">할인 (%)</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">값 ($)</td>
                        </tr> 
                        </tbody>
                        ###<productList>###
                        </table>
					</div>

					<div style="padding: 1vw;">
						<table class="printFont" style="font-size: 1vw;">
							<tbody><tr>
								<td style="padding: 0 1vw;" class="firstColumn">합계</td>
								<td style="padding: 0 1vw;" class="secondColumn"></td>
								<td style="padding: 0 1vw;" class="thirdColumn total">###<total>###</td>
							</tr>
							<tr>
								<td style="padding: 0 1vw;" class="firstColumn">세금 (6%)</td>
								<td style="padding: 0 1vw;" class="secondColumn"></td>
								<td style="padding: 0 1vw;" class="thirdColumn tax">###<tax>###</td>
							</tr>
							<tr>
								<td style="padding: 0 1vw;" class="firstColumn">그랜드 할인  %</td>
								<td style="padding: 0 1vw;" class="secondColumn"></td>
								<td style="padding: 0 1vw;" class="thirdColumn grandDiscount">###<grandDiscount>###</td>
							</tr>
							<tr style="border-top-width: 0.2vw; border-bottom-width: 0.2vw; border-color: black;">
								<td style="padding: 0 1vw; font-weight: bold;" class="firstColumn borderUpBottom">총계</td>
								<td style="padding: 0 1vw;" class="secondColumn borderUpBottom"></td>
								<td style="padding: 0 1vw; font-weight: bold;" class="thirdColumn grandTotal borderUpBottom">###<grandTotal>###</td>
							</tr>
						</tbody></table>
					</div>

					<div>
						<span style="font-size: 1vw;" class="printFont">다음 약속 : </span><span class="printFont nextAppointmentDate" style="font-size: 1vw; font-weight: bold;">###<nextApptDate>###</span>
					</div>
					<div>
						<span style="font-size: 1vw;" class="printFont">수술/예방 접종/상담 이름 : </span><span class="printFont nextAppointmentName" style="font-size: 1vw; font-weight: bold;">###<nextApptName>###</span>
					</div>

				</div>
			</div>
		</div>
	</div>
',
NOW(), 'SYSTEM'),
((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'ja', '請求書の作成',
'
<style>    
	.modal-content-custom {
		border: 0.1vw solid !important;
		padding: 1vw;
	}
    
    .firstColumn {
		width: 20vw !important;
	}

	.secondColumn {
		width: 14vw !important;
	}

	.thirdColumn {
		width: 10vw !important;
	}
    
    table{
		border-collapse: collapse;
	}

	td.borderBelow {
		border-style: solid;
		border-width: 0;
		border-bottom-width: 0.1vw;
	}

	td.borderUpBottom {
		border-style: solid;
		border-width: 0;
		border-bottom-width: 0.1vw;
		border-top-width: 0.1vw;
	}

	.modalHeader{
		padding: 0 1vw !important;
		border-bottom: 1px solid #dee2e6;
		margin-bottom: 1vw;
	}

	.addressContainer{
		width: 19vw !important;
	}
    
    .modal-content-custom {
		border: 0.1vw solid !important;
		padding: 1vw;
	}
</style>

  <div class="modal-dialog" style="margin: 1vw auto; width: fit-content;">
		<div class="modal-content modal-content-custom">
			<div class="modal-header modalHeader" style="display: flex; justify-content: space-between; align-items: center;">
				<div style="font-weight: bold;" class="printFont invoiceReceiptNo">###<invoiceReceiptNoLabel>###: ###<invoiceReceiptNo>###</div>
			</div>

			<div class="modal-body" style="display: flex;">
				<div style="margin: 0 1vw;">
					<div>
						<span style="font-size: 1vw;" class="printFont ownerName">###<ownerName>###</span>
					</div>

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">連絡先番号</span>
						<span class="printFont ownerNo" style="color: black; font-size: 1vw;">###<contactNo>###</span>
					</div>

					<div class="addressContainer" style="display: flex; flex-direction: column; margin: 1vw 0; width: 15vw;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">住所</span>
						<span class="printFont ownerAddress" style="color: black; font-size: 1vw;">###<address>### </span>
					</div>

					<hr style="border: 0.1vw solid black;">

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">ペットネーム</span>
						<span class="printFont petName" style="color: black; font-size: 1vw;">###<petName>###</span>
					</div>

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">登録番号</span>
						<span class="printFont petRegistrationNo" style="color: black; font-size: 1vw;">###<registrationNo>###</span>
					</div>

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">種</span>
						<span class="printFont petSpecies" style="color: black; font-size: 1vw;">###<species>###</span>
					</div>

				</div>
				

				<div>
					<div>
						<span style="font-size: 1vw; font-weight: bold;" class="printFont">サービス名 : </span><span class="printFont serviceName" style="font-size: 1vw;">###<serviceName>###</span>
					</div>
					<div style="margin-bottom: 1vw;">
						<span style="font-size: 1vw; font-weight: bold;" class="printFont">日付 : </span><span class="printFont serviceDate" style="font-size: 1vw;">###<serviceDate>###</span>
					</div>

					<div style="margin-bottom: 1vw; padding: 1vw; background-color: whitesmoke;">
						<table class="printFont" id="serviceList" style="font-size: 1vw;">
                        <tbody>
                        <tr style="border-bottom-width: 0.1vw;">
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">サービス</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">割引 (%)</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">価格 ($)</td>
                        </tr> 
                        </tbody>
                        ###<serviceList>###
                        </table>
					</div>

					<div style="padding: 1vw; background-color: whitesmoke;">
						<table class="printFont" id="productList" style="font-size: 1vw;">
                        <tbody>
                        <tr style="border-bottom-width: 0.1vw;">
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">積</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">割引 (%)</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">価格 ($)</td>
                        </tr> 
                        </tbody>
                        ###<productList>###
                        </table>
					</div>

					<div style="padding: 1vw;">
						<table class="printFont" style="font-size: 1vw;">
							<tbody><tr>
								<td style="padding: 0 1vw;" class="firstColumn">合計</td>
								<td style="padding: 0 1vw;" class="secondColumn"></td>
								<td style="padding: 0 1vw;" class="thirdColumn total">###<total>###</td>
							</tr>
							<tr>
								<td style="padding: 0 1vw;" class="firstColumn">税金 (6%)</td>
								<td style="padding: 0 1vw;" class="secondColumn"></td>
								<td style="padding: 0 1vw;" class="thirdColumn tax">###<tax>###</td>
							</tr>
							<tr>
								<td style="padding: 0 1vw;" class="firstColumn">グランドディスカウント %</td>
								<td style="padding: 0 1vw;" class="secondColumn"></td>
								<td style="padding: 0 1vw;" class="thirdColumn grandDiscount">###<grandDiscount>###</td>
							</tr>
							<tr style="border-top-width: 0.2vw; border-bottom-width: 0.2vw; border-color: black;">
								<td style="padding: 0 1vw; font-weight: bold;" class="firstColumn borderUpBottom">総計</td>
								<td style="padding: 0 1vw;" class="secondColumn borderUpBottom"></td>
								<td style="padding: 0 1vw; font-weight: bold;" class="thirdColumn grandTotal borderUpBottom">###<grandTotal>###</td>
							</tr>
						</tbody></table>
					</div>

					<div>
						<span style="font-size: 1vw;" class="printFont">次回の予約 : </span><span class="printFont nextAppointmentDate" style="font-size: 1vw; font-weight: bold;">###<nextApptDate>###</span>
					</div>
					<div>
						<span style="font-size: 1vw;" class="printFont">手術/予防接種/診察名 : </span><span class="printFont nextAppointmentName" style="font-size: 1vw; font-weight: bold;">###<nextApptName>###</span>
					</div>

				</div>
			</div>
		</div>
	</div>
',
NOW(), 'SYSTEM'),
((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'zh-Hans', '发票创建',
'
<style>    
	.modal-content-custom {
		border: 0.1vw solid !important;
		padding: 1vw;
	}
    
    .firstColumn {
		width: 20vw !important;
	}

	.secondColumn {
		width: 14vw !important;
	}

	.thirdColumn {
		width: 10vw !important;
	}
    
    table{
		border-collapse: collapse;
	}

	td.borderBelow {
		border-style: solid;
		border-width: 0;
		border-bottom-width: 0.1vw;
	}

	td.borderUpBottom {
		border-style: solid;
		border-width: 0;
		border-bottom-width: 0.1vw;
		border-top-width: 0.1vw;
	}

	.modalHeader{
		padding: 0 1vw !important;
		border-bottom: 1px solid #dee2e6;
		margin-bottom: 1vw;
	}

	.addressContainer{
		width: 19vw !important;
	}
    
    .modal-content-custom {
		border: 0.1vw solid !important;
		padding: 1vw;
	}
</style>

  <div class="modal-dialog" style="margin: 1vw auto; width: fit-content;">
		<div class="modal-content modal-content-custom">
			<div class="modal-header modalHeader" style="display: flex; justify-content: space-between; align-items: center;">
				<div style="font-weight: bold;" class="printFont invoiceReceiptNo">###<invoiceReceiptNoLabel>###: ###<invoiceReceiptNo>###</div>
			</div>

			<div class="modal-body" style="display: flex;">
				<div style="margin: 0 1vw;">
					<div>
						<span style="font-size: 1vw;" class="printFont ownerName">###<ownerName>###</span>
					</div>

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">联系电话</span>
						<span class="printFont ownerNo" style="color: black; font-size: 1vw;">###<contactNo>###</span>
					</div>

					<div class="addressContainer" style="display: flex; flex-direction: column; margin: 1vw 0; width: 15vw;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">地址</span>
						<span class="printFont ownerAddress" style="color: black; font-size: 1vw;">###<address>### </span>
					</div>

					<hr style="border: 0.1vw solid black;">

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">爱称</span>
						<span class="printFont petName" style="color: black; font-size: 1vw;">###<petName>###</span>
					</div>

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">注册号码</span>
						<span class="printFont petRegistrationNo" style="color: black; font-size: 1vw;">###<registrationNo>###</span>
					</div>

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">物种</span>
						<span class="printFont petSpecies" style="color: black; font-size: 1vw;">###<species>###</span>
					</div>

				</div>
				

				<div>
					<div>
						<span style="font-size: 1vw; font-weight: bold;" class="printFont">服务名称 : </span><span class="printFont serviceName" style="font-size: 1vw;">###<serviceName>###</span>
					</div>
					<div style="margin-bottom: 1vw;">
						<span style="font-size: 1vw; font-weight: bold;" class="printFont">日期 : </span><span class="printFont serviceDate" style="font-size: 1vw;">###<serviceDate>###</span>
					</div>

					<div style="margin-bottom: 1vw; padding: 1vw; background-color: whitesmoke;">
						<table class="printFont" id="serviceList" style="font-size: 1vw;">
                        <tbody>
                        <tr style="border-bottom-width: 0.1vw;">
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">服务</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">折扣  (%)</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">价格  ($)</td>
                        </tr> 
                        </tbody>
                        ###<serviceList>###
                        </table>
					</div>

					<div style="padding: 1vw; background-color: whitesmoke;">
						<table class="printFont" id="productList" style="font-size: 1vw;">
                        <tbody>
                        <tr style="border-bottom-width: 0.1vw;">
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">产品</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">折扣  (%)</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">价格  ($)</td>
                        </tr> 
                        </tbody>
                        ###<productList>###
                        </table>
					</div>

					<div style="padding: 1vw;">
						<table class="printFont" style="font-size: 1vw;">
							<tbody><tr>
								<td style="padding: 0 1vw;" class="firstColumn">总</td>
								<td style="padding: 0 1vw;" class="secondColumn"></td>
								<td style="padding: 0 1vw;" class="thirdColumn total">###<total>###</td>
							</tr>
							<tr>
								<td style="padding: 0 1vw;" class="firstColumn">税 (6%)</td>
								<td style="padding: 0 1vw;" class="secondColumn"></td>
								<td style="padding: 0 1vw;" class="thirdColumn tax">###<tax>###</td>
							</tr>
							<tr>
								<td style="padding: 0 1vw;" class="firstColumn">大折扣 %</td>
								<td style="padding: 0 1vw;" class="secondColumn"></td>
								<td style="padding: 0 1vw;" class="thirdColumn grandDiscount">###<grandDiscount>###</td>
							</tr>
							<tr style="border-top-width: 0.2vw; border-bottom-width: 0.2vw; border-color: black;">
								<td style="padding: 0 1vw; font-weight: bold;" class="firstColumn borderUpBottom">总计</td>
								<td style="padding: 0 1vw;" class="secondColumn borderUpBottom"></td>
								<td style="padding: 0 1vw; font-weight: bold;" class="thirdColumn grandTotal borderUpBottom">###<grandTotal>###</td>
							</tr>
						</tbody></table>
					</div>

					<div>
						<span style="font-size: 1vw;" class="printFont">下次预约 : </span><span class="printFont nextAppointmentDate" style="font-size: 1vw; font-weight: bold;">###<nextApptDate>###</span>
					</div>
					<div>
						<span style="font-size: 1vw;" class="printFont">手术/疫苗接种/咨询名称 : </span><span class="printFont nextAppointmentName" style="font-size: 1vw; font-weight: bold;">###<nextApptName>###</span>
					</div>

				</div>
			</div>
		</div>
	</div>
',
NOW(), 'SYSTEM'),
((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN003'),
'zh-Hant', '發票創建',
'
<style>    
	.modal-content-custom {
		border: 0.1vw solid !important;
		padding: 1vw;
	}
    
    .firstColumn {
		width: 20vw !important;
	}

	.secondColumn {
		width: 14vw !important;
	}

	.thirdColumn {
		width: 10vw !important;
	}
    
    table{
		border-collapse: collapse;
	}

	td.borderBelow {
		border-style: solid;
		border-width: 0;
		border-bottom-width: 0.1vw;
	}

	td.borderUpBottom {
		border-style: solid;
		border-width: 0;
		border-bottom-width: 0.1vw;
		border-top-width: 0.1vw;
	}

	.modalHeader{
		padding: 0 1vw !important;
		border-bottom: 1px solid #dee2e6;
		margin-bottom: 1vw;
	}

	.addressContainer{
		width: 19vw !important;
	}
    
    .modal-content-custom {
		border: 0.1vw solid !important;
		padding: 1vw;
	}
</style>

  <div class="modal-dialog" style="margin: 1vw auto; width: fit-content;">
		<div class="modal-content modal-content-custom">
			<div class="modal-header modalHeader" style="display: flex; justify-content: space-between; align-items: center;">
				<div style="font-weight: bold;" class="printFont invoiceReceiptNo">###<invoiceReceiptNoLabel>###: ###<invoiceReceiptNo>###</div>
			</div>

			<div class="modal-body" style="display: flex;">
				<div style="margin: 0 1vw;">
					<div>
						<span style="font-size: 1vw;" class="printFont ownerName">###<ownerName>###</span>
					</div>

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">聯絡電話</span>
						<span class="printFont ownerNo" style="color: black; font-size: 1vw;">###<contactNo>###</span>
					</div>

					<div class="addressContainer" style="display: flex; flex-direction: column; margin: 1vw 0; width: 15vw;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">地址</span>
						<span class="printFont ownerAddress" style="color: black; font-size: 1vw;">###<address>### </span>
					</div>

					<hr style="border: 0.1vw solid black;">

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">愛稱</span>
						<span class="printFont petName" style="color: black; font-size: 1vw;">###<petName>###</span>
					</div>

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">註冊號</span>
						<span class="printFont petRegistrationNo" style="color: black; font-size: 1vw;">###<registrationNo>###</span>
					</div>

					<div style="display: flex; flex-direction: column; margin: 1vw 0;">
						<span class="printFont" style="color: darkgray; font-size: 1vw;">種</span>
						<span class="printFont petSpecies" style="color: black; font-size: 1vw;">###<species>###</span>
					</div>

				</div>
				

				<div>
					<div>
						<span style="font-size: 1vw; font-weight: bold;" class="printFont">服務名稱 : </span><span class="printFont serviceName" style="font-size: 1vw;">###<serviceName>###</span>
					</div>
					<div style="margin-bottom: 1vw;">
						<span style="font-size: 1vw; font-weight: bold;" class="printFont">日期 : </span><span class="printFont serviceDate" style="font-size: 1vw;">###<serviceDate>###</span>
					</div>

					<div style="margin-bottom: 1vw; padding: 1vw; background-color: whitesmoke;">
						<table class="printFont" id="serviceList" style="font-size: 1vw;">
                        <tbody>
                        <tr style="border-bottom-width: 0.1vw;">
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">服務</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">折扣 (%)</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">價格 ($)</td>
                        </tr> 
                        </tbody>
                        ###<serviceList>###
                        </table>
					</div>

					<div style="padding: 1vw; background-color: whitesmoke;">
						<table class="printFont" id="productList" style="font-size: 1vw;">
                        <tbody>
                        <tr style="border-bottom-width: 0.1vw;">
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">產品</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">折扣 (%)</td>
                        <td class="borderBelow" style="font-weight: bold; padding: 0 1vw;">價格 ($)</td>
                        </tr> 
                        </tbody>
                        ###<productList>###
                        </table>
					</div>

					<div style="padding: 1vw;">
						<table class="printFont" style="font-size: 1vw;">
							<tbody><tr>
								<td style="padding: 0 1vw;" class="firstColumn">總</td>
								<td style="padding: 0 1vw;" class="secondColumn"></td>
								<td style="padding: 0 1vw;" class="thirdColumn total">###<total>###</td>
							</tr>
							<tr>
								<td style="padding: 0 1vw;" class="firstColumn">稅 (6%)</td>
								<td style="padding: 0 1vw;" class="secondColumn"></td>
								<td style="padding: 0 1vw;" class="thirdColumn tax">###<tax>###</td>
							</tr>
							<tr>
								<td style="padding: 0 1vw;" class="firstColumn">大折扣 %</td>
								<td style="padding: 0 1vw;" class="secondColumn"></td>
								<td style="padding: 0 1vw;" class="thirdColumn grandDiscount">###<grandDiscount>###</td>
							</tr>
							<tr style="border-top-width: 0.2vw; border-bottom-width: 0.2vw; border-color: black;">
								<td style="padding: 0 1vw; font-weight: bold;" class="firstColumn borderUpBottom">總計</td>
								<td style="padding: 0 1vw;" class="secondColumn borderUpBottom"></td>
								<td style="padding: 0 1vw; font-weight: bold;" class="thirdColumn grandTotal borderUpBottom">###<grandTotal>###</td>
							</tr>
						</tbody></table>
					</div>

					<div>
						<span style="font-size: 1vw;" class="printFont">下次预约 : </span><span class="printFont nextAppointmentDate" style="font-size: 1vw; font-weight: bold;">###<nextApptDate>###</span>
					</div>
					<div>
						<span style="font-size: 1vw;" class="printFont">手术/疫苗接种/咨询名称 : </span><span class="printFont nextAppointmentName" style="font-size: 1vw; font-weight: bold;">###<nextApptName>###</span>
					</div>

				</div>
			</div>
		</div>
	</div>
',
NOW(), 'SYSTEM');

-- VPMS_EN004
/*----------- Email : Invoice Notification --------------*/
INSERT INTO mst_template(TemplateType, TemplateCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES('Email Notification', 'VPMS_EN004', 'Invoice ###<invoiceno>### from Vet Vitals PMS', '', 
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN004'),
'de', 'Rechnung ###<invoiceno>### von Vet Vitals PMS',
'<!DOCTYPE html>
<html>
 	<body>
		Sehr geehrter ###<customer>###, </br></br>

		Anbei finden Sie die Rechnung mit der Nummer <strong>###<invoiceno>###</strong> für Ihre letzten Besuche in unserer Klinik. Bitte überprüfen Sie diese und bezahlen Sie anschließend am Schalter.</br></br>

		Bitte antworten Sie nicht auf diese E-Mail, da sie automatisch generiert wird.</br></br>

		Mit freundlichen Grüßen</br>
		VPMS-Team</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN004'),
'en', 'Invoice ###<invoiceno>### from Vet Vitals PMS',
'<!DOCTYPE html>
<html>
 	<body>
 		Dear ###<customer>###, </br></br>

 		Please find attached invoice number <strong>###<invoiceno>###</strong> for your recent visits to our clinic. Please kindly review and make the payment at counter.</br></br>

 		Please refrain from replying to this email as it is auto-generated.</br></br>

 		Best regards,</br>
 		VPMS Team</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN004'),
'es', 'Factura ###<invoiceno>### de Vet Vitals PMS',
'<!DOCTYPE html>
<html>
 	<body>
		Estimado/a ###<customer>###:</br></br>

		Adjunto la factura número <strong>###<invoiceno>###</strong> de sus visitas recientes a nuestra clínica. Por favor, revísela y realice el pago en ventanilla.</br></br>

		No responda a este correo electrónico, ya que se genera automáticamente.</br></br>

		Atentamente,</br>
		Equipo VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN004'),
'fr', 'Facture ###<invoiceno>### de Vet Vitals PMS',
'<!DOCTYPE html>
<html>
 	<body>
		Cher ###<customer>###, </br></br>

		Veuillez trouver ci-joint la facture numéro <strong>###<invoiceno>###</strong> relative à vos récentes visites à notre clinique. Veuillez la consulter et effectuer le paiement au comptoir.</br></br>

		Veuillez ne pas répondre à cet e-mail, car il est généré automatiquement.</br></br>

		Cordialement,</br>
		Équipe VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN004'),
'hi', 'Vet Vitals PMS से चालान ###<invoiceno>###',
'<!DOCTYPE html>
<html>
 	<body>
		प्रिय ###<customer>###, </br></br>

		कृपया हमारे क्लिनिक में अपनी हाल की यात्राओं के लिए संलग्न चालान संख्या <strong>###<invoiceno>###</strong> देखें। कृपया समीक्षा करें और काउंटर पर भुगतान करें।</br></br>

		कृपया इस ईमेल का उत्तर देने से बचें क्योंकि यह स्वतः जनरेट किया गया है।</br></br>

		सादर,</br>
		VPMS टीम</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN004'),
'ja', 'Vet Vitals PMS からの請求書 ###<invoiceno>###',
'<!DOCTYPE html>
<html>
 	<body>
		###<customer>### 様、</br></br>

		最近の当クリニックへのご来院に対する請求書番号 <strong>###<invoiceno>###</strong> を添付しましたのでご確認ください。 ご確認のうえ、カウンターでお支払いください。</br></br>

		このメールは自動生成のため、返信は控えてください。</br></br>

		よろしくお願いいたします。</br>

		VPMS チーム</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN004'),
'ko', 'Vet Vitals PMS의 송장 ###<invoiceno>###',
'<!DOCTYPE html>
<html>
 	<body>
		친애하는 ###<customer>###님, </br></br>

		최근 저희 병원을 방문하신 것에 대한 송장 번호 <strong>###<invoiceno>###</strong>를 첨부했습니다. 카운터에서 검토하고 지불해 주시기 바랍니다.</br></br>

		이 이메일은 자동 생성되므로 답장하지 마십시오.</br></br>

		감사합니다.</br>
		VPMS 팀</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN004'),
'pt', 'Fatura ###<invoiceno>### da Vet Vitals PMS',
'<!DOCTYPE html>
<html>
 	<body>
		Caro ###<customer>###, </br></br>

		Em anexo, encontra a fatura número <strong>###<invoiceno>###</strong> referente às suas recentes visitas à nossa clínica. Por favor, reveja e efetue o pagamento ao balcão.</br></br>

		Por favor, evite responder a este e-mail, pois é gerado automaticamente.</br></br>

		Atenciosamente,</br>
		Equipa VPMS
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN004'),
'ru', 'Счет №##<invoiceno>### от Vet Vitals PMS',
'<!DOCTYPE html>
<html>
 	<body>
		Уважаемый ###<customer>###, </br></br>

		В приложении вы найдете номер счета <strong>###<invoiceno>###</strong> за ваши недавние визиты в нашу клинику. Пожалуйста, проверьте и произведите оплату на стойке.</br></br>

		Пожалуйста, воздержитесь от ответа на это письмо, так как оно генерируется автоматически.</br></br>

		С наилучшими пожеланиями,</br>
		Команда VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN004'),
'vi', 'Hóa đơn ###<invoiceno>### từ Vet Vitals PMS',
'<!DOCTYPE html>
<html>
 	<body>
		Kính gửi ###<customer>###, </br></br>

		Vui lòng tìm số hóa đơn đính kèm <strong>###<invoiceno>###</strong> cho những lần khám gần đây của bạn tại phòng khám của chúng tôi. Vui lòng xem lại và thanh toán tại quầy.</br></br>

		Vui lòng không trả lời email này vì nó được tạo tự động.</br></br>

		Trân trọng,</br>
		Nhóm VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN004'),
'zh-Hans', '来自 Vet Vitals PMS 的发票 ###<invoiceno>###',
'<!DOCTYPE html>
<html>
 	<body>
		尊敬的###<customer>###，</br></br>

		请查阅附件中您最近来我们诊所就诊的发票号码<strong>###<invoiceno>###</strong>。请仔细阅读并在柜台付款。</br></br>

		请不要回复此电子邮件，因为它是自动生成的。</br></br>

		此致，</br>
		VPMS 团队</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN004'),
'zh-Hant', '來自 Vet Vitals PMS 的發票 ###<invoiceno>###',
'<!DOCTYPE html>
<html>
 	<body>
		親愛的###<customer>###，</br></br>

		請參閱附件中您最近到我們診所就診的發票號碼<strong><invoiceno>###</strong>。請檢查並在櫃檯付款。 </br></br>

		請不要回覆此電子郵件，因為它是自動產生的。 </br></br>

		此致敬禮，</br>
		VPMS 團隊
 	</body>
</html>',
NOW(), 'SYSTEM');


-- VPMS_EN005
/*----------- Email : Receipt Notification --------------*/
INSERT INTO mst_template(TemplateType, TemplateCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES('Email Notification', 'VPMS_EN005', 'Receipts ###<receiptno>### from Vet Vitals PMS', '', 
NOW(), 'SYSTEM');


INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN005'),
'de', 'Quittungen ###<receiptno>### von Vet Vitals PMS',
'<!DOCTYPE html>
<html>
 	<body>
		Sehr geehrter ###<customer>###, </br></br>

		Zahlung für Rechnung Nr. <strong>###<invoiceno.>###</strong> eingegangen. Die beigefügte Quittung dient als Referenz.</br></br>

		Bitte antworten Sie nicht auf diese E-Mail, da sie automatisch generiert wird.</br></br>

		Mit freundlichen Grüßen</br>
		VPMS-Team</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN005'),
'en', 'Receipts ###<receiptno>### from Vet Vitals PMS',
'<!DOCTYPE html>
<html>
 	<body>
 		Dear ###<customer>###, </br></br>

 		Payment for invoice no <strong>###<invoiceno>###</strong> received. Please find the attached receipt for your reference.</br></br>

 		Please refrain from replying to this email as it is auto-generated.</br></br>

 		Best regards,</br>
 		VPMS Team</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN005'),
'es', 'Recibos ###<receiptno>### de Vet Vitals PMS',
'<!DOCTYPE html>
<html>
 	<body>
		Estimado/a ###<customer>###:</br></br>

		He recibido el pago de la factura n.° <strong>###<invoiceno>###</strong>. Adjunto el recibo.</br></br>

		No responda a este correo electrónico, ya que se genera automáticamente.</br></br>

		Atentamente,</br>
		Equipo VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN005'),
'fr', 'Reçus ###<receiptno>### de Vet Vitals PMS',
'<!DOCTYPE html>
<html>
 	<body>
		Cher ###<customer>###, </br></br>

		Paiement de la facture n° <strong>###<invoiceno>###</strong> reçu. Veuillez trouver le reçu ci-joint pour référence.</br></br>

		Veuillez ne pas répondre à cet e-mail, car il est généré automatiquement.</br></br>

		Cordialement,</br>
		Équipe VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN005'),
'hi', 'रसीदें ###<receiptno>### Vet Vitals PMS से',
'<!DOCTYPE html>
<html>
 	<body>
		प्रिय ###<customer>###, </br></br>

		चालान संख्या <strong>###<invoiceno>###</strong> के लिए भुगतान प्राप्त हुआ। कृपया अपने संदर्भ के लिए संलग्न रसीद देखें।</br></br>

		कृपया इस ईमेल का उत्तर देने से बचें क्योंकि यह स्वतः जनरेट किया गया है।</br></br>

		सादर,</br>
		VPMS टीम</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN005'),
'id', 'Tanda terima ###<receiptno>### dari Vet Vitals PMS',
'<!DOCTYPE html>
<html>
 	<body>
		Kepada ###<customer>###, </br></br>

		Pembayaran untuk faktur no <strong>###<invoiceno>###</strong> telah diterima. Harap temukan tanda terima terlampir untuk referensi Anda.</br></br>

		Harap jangan membalas email ini karena email ini dibuat secara otomatis.</br></br>

		Salam,</br>
		Tim VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN005'),
'ja', 'Vet Vitals PMS からの領収書 ###<receiptno>###',
'<!DOCTYPE html>
<html>
 	<body>
		###<customer>### 様、</br></br>

		請求書番号 <strong>###<invoiceno>###</strong> の支払いを受け取りました。添付の領収書をご確認の上、ご参照ください。</br></br>

		このメールは自動生成されているため、返信は控えてください。</br></br>

		よろしくお願いいたします。</br>

		VPMS チーム</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN005'),
'ko', 'Vet Vitals PMS에서 받은 영수증 ###<receiptno>###',
'<!DOCTYPE html>
<html>
 	<body>
		친애하는 ###<customer>###님, </br></br>

		송장 번호 <strong>###<invoiceno>###</strong>에 대한 지불이 접수되었습니다. 참조를 위해 첨부된 영수증을 확인하세요.</br></br>

		이 이메일은 자동 생성되므로 답장하지 마세요.</br></br>

		감사합니다.</br>
		VPMS 팀</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN005'),
'pt', 'Recibos ###<receiptno>### da Vet Vitals PMS',
'<!DOCTYPE html>
<html>
 	<body>
		Caro ###<customer>###, </br></br>

		Pagamento da fatura n.º <strong>###<invoiceno>###</strong> recebido. Encontre o recibo em anexo para sua referência.</br></br>

		Por favor, evite responder a este e-mail, pois é gerado automaticamente.</br></br>

		Atenciosamente,</br>
		Equipa VPMS
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN005'),
'ru', 'Квитанции ###<receiptno>### от Vet Vitals PMS',
'<!DOCTYPE html>
<html>
 	<body>
		Уважаемый ###<customer>###, </br></br>

		Оплата по счету № <strong>###<invoiceno>###</strong> получена. Пожалуйста, найдите приложенную квитанцию ​​для справки.</br></br>

		Пожалуйста, воздержитесь от ответа на это письмо, так как оно генерируется автоматически.</br></br>

		С наилучшими пожеланиями,</br>
		Команда VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN005'),
'vi', 'Biên lai ###<receiptno>### từ Vet Vitals PMS',
'<!DOCTYPE html>
<html>
 	<body>
		Kính gửi ###<customer>###, </br></br>

		Đã nhận được thanh toán cho hóa đơn số <strong>###<invoiceno>###</strong>. Vui lòng tìm biên lai đính kèm để tham khảo.</br></br>

		Vui lòng không trả lời email này vì nó được tạo tự động.</br></br>

		Trân trọng,</br>
		Nhóm VPMS</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN005'),
'zh-Hans', '来自 Vet Vitals PMS 的收据 ###<receiptno>###',
'<!DOCTYPE html>
<html>
 	<body>
		尊敬的 ###<customer>###，</br></br>

		已收到发票号 <strong>###<invoiceno>###</strong> 的付款。请参阅附件收据以供参考。</br></br>

		请不要回复此电子邮件，因为它是自动生成的。</br></br>

		此致，</br>
		VPMS 团队</br>
 	</body>
</html>',
NOW(), 'SYSTEM');

INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN005'),
'zh-Hant', '來自 Vet Vitals PMS 的收據 ###<receiptno>###',
'<!DOCTYPE html>
<html>
 	<body>
		親愛的###<customer>###，</br></br>

		已收到發票號碼 <strong><invoiceno>###</strong> 的付款。請參閱附件收據以供參考。 </br></br>

		請不要回覆此電子郵件，因為它是自動產生的。 </br></br>

		此致敬禮，</br>
		VPMS 團隊
 	</body>
</html>',
NOW(), 'SYSTEM');

-- VPMS_EN010 
/*---------- Email : New Account Creation ----------------*/
INSERT INTO mst_template(TemplateType, TemplateCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES('Email Notification', 'VPMS_EN010', 'New Login Account Creation', '', NOW(), 'SYSTEM');


INSERT INTO mst_template_details(TemplateID, LangCode, TemplateTitle, TemplateContent, CreatedDate, CreatedBy)
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'),
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
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'),
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
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'),
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
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'),
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
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'),
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
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'),
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
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'),
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
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'),
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
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'),
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
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'),
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
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'),
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
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'),
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
VALUES((SELECT TemplateID FROM mst_template WHERE TemplateCode='VPMS_EN010'),
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

-- CPMS_EN001
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

-- CPMS_EN002
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

-- CPMS_EN003
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
