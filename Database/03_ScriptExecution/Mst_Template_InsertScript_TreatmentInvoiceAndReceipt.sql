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