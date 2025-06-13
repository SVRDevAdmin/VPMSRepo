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