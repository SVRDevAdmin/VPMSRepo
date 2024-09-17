var sCalendarView = 'dayGridMonth';
if (sessionStorage.getItem("CalendarView") != null) {
    sCalendarView = sessionStorage.getItem("CalendarView");
}

var calendar;
document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('apptCalendar');
    var viewDropdownMenu = document.getElementById('viewDropdownMenu');

    calendar = new FullCalendar.Calendar(calendarEl, {
        headerToolbar: {
            left: '',
            center: 'title',
            right: 'prev,next,customButton'
        },
        initialView: 'dayGridMonth',
        views: {
            dayGridMonth: {
                titleFormat: {
                    year: 'numeric',
                    month: 'long'
                },
                buttonIcons: {
                    prev: 'chevron-left',
                    next: 'chevron-right',
                    customButton: 'chevron-down'
                },
                dayHeaderFormat: {
                    weekday: 'long'
                },
                displayEventTime: false,
                eventClick: function (args) {
                    $('#lbTitle').html('');
                    $('#lbTime').html('');
                    $('#lbDoctor').html('');
                    $('#lbPet').html('');
                    $('#lbOwner').html('');

                    var extendedObj = args.event._def.extendedProps;
                    $('#hidApptID').val(extendedObj.apptid);
                    $('#lbTitle').html(args.event._def.title);
                    $('#lbTime').html(moment(extendedObj.starttime, 'hh:mm:ss').format('hh:mmA') + ' - ' +
                        moment(extendedObj.endtime, 'hh:mm:ss').format('hh:mmA'));
                    $('#lbDoctor').html(extendedObj.doctor);
                    $('#lbPet').html(extendedObj.Pet);
                    $('#lbOwner').html(extendedObj.Owner);

                    $('#EventModal').modal('show');
                }
            },
            timeGridWeek: {
                titleFormat: {
                    year: 'numeric',
                    month: 'long'
                },
                dayHeaderFormat: {
                    weekday: 'long',
                    day: '2-digit'
                },
                displayEventTime: false,
                allDaySlot: false,
                slotMinTime: "08:00:00",
                slotMaxTime: "24:00:00",
                slotDuration: "00:15:00",
                slotLabelFormat: {
                    hour: 'numeric',
                    minute: '2-digit',
                    hour12: false
                },
                slotEventOverlap: false,
                dayHeaderContent: function (args) {
                    const dayHeaderHtml = "<div>" + moment(args.date).format("ddd") +
                        "<br/>" + moment(args.date).format("D") +
                        "</div>";
                    return {
                        html: dayHeaderHtml
                    }
                },
                eventContent: function (args, createElement) {
                    const extendedObj = args.event._def.extendedProps;
                    const contentHtml = PopulateWeekViewsEvent(args, extendedObj);
                    //console.info("EventContent:", args.event);
                    return {
                        html: contentHtml
                    };
                },
                eventClick: function (args) {
                    $('#lbTitle').html('');
                    $('#lbTime').html('');
                    $('#lbDoctor').html('');
                    $('#lbPet').html('');
                    $('#lbOwner').html('');

                    var extendedObj = args.event._def.extendedProps;
                    $('#hidApptID').val(extendedObj.apptid);
                    $('#lbTitle').html(args.event._def.title);
                    $('#lbTime').html(moment(extendedObj.starttime, 'hh:mm:ss').format('hh:mmA') + ' - ' +
                        moment(extendedObj.endtime, 'hh:mm:ss').format('hh:mmA'));
                    $('#lbDoctor').html(extendedObj.doctor);
                    $('#lbPet').html(extendedObj.Pet);
                    $('#lbOwner').html(extendedObj.Owner);

                    $('#EventModal').modal('show');
                }
            },
            customDayGrid: {
                type: 'timeGrid',
                titleFormat: {
                    year: 'numeric',
                    month: 'long',
                    day: '2-digit'
                },
                dayHeaders: false,
                displayEventTime: false,
                allDaySlot: false,
                slotMinTime: "08:00:00",
                slotMaxTime: "24:00:00",
                slotDuration: "00:15:00",
                slotLabelFormat: {
                    hour: 'numeric',
                    minute: '2-digit',
                    hour12: false
                },
                slotEventOverlap: false,
                eventContent: function (args, createElement) {
                    const extendedObj = args.event._def.extendedProps;
                    const contentHtml = PopulateCustomViewsEvent(args, extendedObj);
                    //console.info("EventContent:", args.event);
                    return {
                        html: contentHtml
                    }
                }
            }
        },
        customButtons: {
            customButton: {
                text: '@Html.Raw(LangResources["Appointment_Label_Month"])',
                click: function (event) {
                    var rect = event.target.getBoundingClientRect();
                    viewDropdownMenu.style.display = 'block';
                    viewDropdownMenu.style.left = rect.left + 'px';
                    viewDropdownMenu.style.top = (rect.bottom + window.scrollY) + 'px';
                }
            },
            prev: {
                click: function (event) {
                    var n = $('.fc-customButton-button').text();
                    calendar.prev();

                    CalendarViewChanges(calendar, 'dayGridMonth');

                    $('.fc-customButton-button').text(n);
                }
            },
            next: {
                click: function (a) {
                    var n = $('.fc-customButton-button').text();
                    calendar.next();

                    CalendarViewChanges(calendar, 'dayGridMonth');

                    $('.fc-customButton-button').text(n);
                }
            }
        }
    });

    calendar.batchRendering(function () {
        calendar.changeView(sCalendarView);
        CalendarViewChanges(calendar, sCalendarView);
    });

    calendar.render();


    $('#dtAddAppt').datepicker({
        uiLibrary: 'bootstrap5',
        format: 'dd/mm/yyyy'
    });
    $('#dtNewDOB').datepicker({
        uiLibrary: 'bootstrap5',
        format: 'dd/mm/yyyy'
    });
    $('#dtNewApptDate').datepicker({
        uiLibrary: 'bootstrap5',
        format: 'dd/mm/yyyy'
    });
    $('#dtEditAppt').datepicker({
        uiLibrary: 'bootstrap5',
        format: 'dd/mm/yyyy'
    });


    $('#strtimePicker').datetimepicker({
        useCurrent: false,
        format: 'HH:mm',
        icons: {
            up: 'fa fa-angle-up',
            down: 'fa fa-angle-down'
        }
    }).on('dp.show', function () {
        time = "09:00 AM";
        $(this).data('DateTimePicker').date(time);
        $(this).data('DateTimePicker').minDate(time);
    }).on('dp.change', function () {
        var min_endtime = $(this).val();
        var min_time = moment(min_endtime, "HH:mm TT").add(30, 'minutes').format('HH:mm');

        $('#endtimePicker').val(min_time);
    });

    $('#endtimePicker').datetimepicker({
        useCurrent: false,
        format: 'HH:mm',
        icons: {
            up: 'fa fa-angle-up',
            down: 'fa fa-angle-down'
        }
    }).on('dp.show', function () {
        var starttime = $('#strtimePicker').val();
        var min_starttime = moment(starttime, "HH:mm TT").add(30, 'minutes').format('HH:mm');

        $(this).data('DateTimePicker').date(min_starttime);
        $(this).data('DateTimePicker').minDate(min_starttime);
    });

    $('#newStrtimePicker').datetimepicker({
        useCurrent: false,
        format: 'HH:mm',
        icons: {
            up: 'fa fa-angle-up',
            down: 'fa fa-angle-down'
        }
    }).on('dp.show', function () {
        time = "09:00 AM";
        $(this).data('DateTimePicker').date(time);
        $(this).data('DateTimePicker').minDate(time);
    }).on('dp.change', function () {
        var min_endtime = $(this).val();
        var min_time = moment(min_endtime, "HH:mm TT").add(30, 'minutes').format('HH:mm');

        $('#newEndtimePicker').val(min_time);
    });

    $('#newEndtimePicker').datetimepicker({
        useCurrent: false,
        format: 'HH:mm',
        icons: {
            up: "fa fa-arrow-up",
            down: "fa fa-arrow-down"
        }
    }).on('dp.show', function () {
        var starttime = $('#newStrtimePicker').val();
        var min_starttime = moment(starttime, "HH:mm TT").add(30, 'minutes').format('HH:mm');

        $(this).data('DateTimePicker').date(min_starttime);
        $(this).data('DateTimePicker').minDate(min_starttime);
    });

    $('#EditStrttimePicker').datetimepicker({
        useCurrent: false,
        format: 'HH:mm',
        icons: {
            up: 'fa fa-angle-up',
            down: 'fa fa-angle-down'
        }
    }).on('dp.show', function () {
        time = "09:00 AM";
        $(this).data('DateTimePicker').date(time);
        $(this).data('DateTimePicker').minDate(time);
    }).on('dp.change', function () {
        var min_endtime = $(this).val();
        var min_time = moment(min_endtime, "HH:mm TT").add(30, 'minutes').format('HH:mm');

        $('#EditEndtimePicker').val(min_time);
    });

    $('#EditEndtimePicker').datetimepicker({
        useCurrent: false,
        format: 'HH:mm',
        icons: {
            up: "fa fa-angle-up",
            down: "fa fa-angle-down"
        }
    }).on('dp.show', function () {
        var starttime = $('#EditStrttimePicker').val();
        var min_starttime = moment(starttime, "HH:mm TT").add(30, 'minutes').format('HH:mm');

        $(this).data('DateTimePicker').date(min_starttime);
        $(this).data('DateTimePicker').minDate(min_starttime);
    });


    $('.dropdown-item').on('click', function (ctl) {
        var sView = $(this)[0].attributes["data"].value;
        calendar.changeView(sView);

        sessionStorage.setItem("CalendarView", sView);

        var sTag = $(this)[0].text;
        $('.fc-customButton-button').text(sTag);

        $('.dropdown-menu').hide();
    });
});

function CalendarViewChanges(calender, viewSelected) {
    var sYr = moment(calendar.currentData.currentDate).format("YYYY");
    var sMth = moment(calendar.currentData.currentDate).format("M");
    var sOwner = $('#ddlSearchOwner').val();
    var sPet = $('#ddlSearchPet').val();
    var sServices = $('#ddlSearchServices').val();
    var sDoctor = $('#ddlSearchDoctor').val();

    $.getJSON("/Appointment/GetCalendarAppointmentsMonthView", {
        sYear: sYr,
        sMonth: sMth,
        searchOwner: sOwner,
        searchPet: sPet,
        searchServices: sServices,
        searchDoctor: sDoctor
    }).done(function (result) {
        if (result.length > 0) {
            calendar.removeAllEvents();

            for (var i = 0; i < result.length; i++) {
                calendar.addEvent({
                    title: result[i].ServiceName,
                    start: moment(result[i].ApptDate).format("YYYY-MM-DD") + 'T' + result[i].ApptStartTimeString,
                    end: moment(result[i].ApptDate).format("YYYY-MM-DD") + 'T' + result[i].ApptEndTimeString,
                    extendedProps: {
                        apptid: result[i].AppointmentID,
                        doctor: result[i].DoctorName,
                        Pet: result[i].PetName,
                        Owner: result[i].OwnerName,
                        starttime: result[i].ApptStartTimeString,
                        endtime: result[i].ApptEndTimeString
                    }
                });

            }
        }
        else {
            calendar.removeAllEvents();
        }
    });
}

function PopulateCustomViewsEvent(args, extendedObj) {
    debugger;
    var sHtmlContent = "<div class='row' style='height: 100% !important; display: flex;'>" +
        "<div class='row' style='border: 0px solid black; margin-top: auto; margin-bottom: auto; vertical-align: middle;'>" +

        "<div style='width: 10%;'>" +
        "<div style='width: 40px; height: 40px; display: flex; margin-left: 15%; margin-right: 10%; margin-top: 15%; margin-bottom: 10%; border-radius: 6px; background-color: white;'>" +
        "<img src='../images/calendar_cat_icon.png' width='20' height='20' style='background-color: transparent; display: block; margin: auto;' />" +
        "</div>" +
        "</div>" +

        "<div style='width: 30%; border-right: 1px solid white;'>" +
        "<div style='margin-left: 20%; margin-right: 10%; margin-top: 5%; margin-bottom: 5%;'>" +
        "<strong>" + args.event._def.title + "</strong><br/>" +
        moment(extendedObj.starttime, 'hh:mm:ss').format('hh:mmA') + " - " + moment(extendedObj.endtime, 'hh:mm:ss').format('hh:mmA') +
        "</div>" +
        "</div>" +

        "<div style='width: 30%; border-right: 1px solid white;'>" +
        "<div style='margin-left: 20%; margin-right: 10%; margin-top: 5%; margin-bottom: 5%;'>" +
        "<strong>" + LangResources['Appointment_Label_Pet'] + " : " + extendedObj.Pet + "</strong><br />" +
            "@Html.Raw(LangResources['Appointment_Label_Owner']) : " + extendedObj.Owner +
                "</div>" +
                "</div>" +

                "<div style='width: 20%; border-right: 1px solid white;'>" +
                "<div style='margin-left: 20%; margin-right: 10%; margin-top: 5%; margin-bottom: 5%;'>" +
                "<strong>" + extendedObj.doctor + "</strong>" +
                "</div>" +
                "</div>" +

                "<div style='width: 10%;'>" +
                "<div style='margin-left: 5%; margin-right: 5%; margin-top: 30%; margin-bottom: 5%; display: flex;'>" +
                "<button id='btnTimeGridEdit' type='button' class='bi bi-pencil-fill' style='color: white; border: 0px; background-color: transparent;' onclick='btnTimeGridEditClick()'></button>" +
                "<button id='btnTimeGridCancel' type='button' class='bi bi-trash3' style='color: white; border: 0px; background-color: transparent;' onclick='btnTimeGridCancelClick()'></button>" +
                "<input id='timeGridApptID' type='text' style='visibility: hidden;' value='" + extendedObj.apptid + "' />" +
                "</div>" +
                "</div>" +

                "</div>" +
                "</div>";

    return sHtmlContent;
}

function PopulateWeekViewsEvent(args, extendedObj) {
    var sHtmlContent = "<div class='row' style='border: 0px solid black; margin-top: auto; margin-bottom: auto; vertical-align: middle;'>" +
        "<div style='width: 100%; font-size: 10px;'>" +
        "<strong>" + args.event._def.title + "</strong><br/>" +
        extendedObj.doctor + "<br/>" +
        "@Html.Raw(LangResources['Appointment_Label_Pet'])  : " + extendedObj.Pet + "<br/>" +
            "@Html.Raw(LangResources['Appointment_Label_Owner']) : " + extendedObj.Owner + "<br/>" +
                "</div>" +
                "</div>";

    return sHtmlContent;
}

function switchAppt(targetModal) {
    $('.apptClss').hide();
    $('#' + targetModal).show();
}

function ddlServicesChanges(services) {
    $.post("/Appointment/GetServiceDoctorList", {
        ServicesID: services.value
    }).done(function (result) {
        loadddlDoctor(result);
    });
}

function ddlAddOwnerchanges(patient) {
    $.post("/Appointment/GetPetListByPatientID", {
        PatientID: patient.value
    }).done(function (result) {
        loadddlPet(result);
    });
}

function ddlSearchOwnerChange(patient) {
    $.post("/Appointment/GetPetListByPatientID", {
        PatientID: patient.value
    }).done(function (result) {
        loadddlSearchPet(result);
    });
}

function ddlNewServicesChanges(services) {
    $.post("/Appointment/GetServiceDoctorList", {
        ServicesID: services.value
    }).done(function (result) {
        loadddlNewDoctor(result);
    });
}

function ddlSearchServicesChange(services) {
    $.post("/Appointment/GetServiceDoctorList", {
        ServicesID: services.value
    }).done(function (result) {
        loadddlSearchDoctor(result);
    });
}

function loadddlDoctor(data) {
    $('#ddlAddDoctor').find("option:not(:first)").remove();
    for (var i = 0; i < data.length; i++) {
        var opt = new Option(data[i].DoctorInCharge, data[i].DoctorInCharge);
        $('#ddlAddDoctor').append(opt);
    }
}

function loadddlNewDoctor(data) {
    $('#ddlNewDoctor').find("option:not(:first)").remove();
    for (var i = 0; i < data.length; i++) {
        var opt = new Option(data[i].DoctorInCharge, data[i].DoctorInCharge);
        $('#ddlNewDoctor').append(opt);
    }
}

function loadddlSearchDoctor(data) {
    $('#ddlSearchDoctor').find("option:not(:first)").remove();
    for (var i = 0; i < data.length; i++) {
        var opt = new Option(data[i].DoctorInCharge, data[i].DoctorInCharge);
        $('#ddlSearchDoctor').append(opt);
    }
}

function loadddlPet(data) {
    $('#ddlAddPet').find("option:not(:first)").remove();
    for (var i = 0; i < data.length; i++) {
        var opt = new Option(data[i].Name, data[i].ID);
        $('#ddlAddPet').append(opt);
    }

    $('#ddlAddPet').val('');
}

function loadddlSearchPet(data) {
    $('#ddlSearchPet').find("option:not(:first)").remove();
    for (var i = 0; i < data.length; i++) {
        var opt = new Option(data[i].Name, data[i].ID);
        $('#ddlSearchPet').append(opt);
    }

    $('#ddlSearchPet').val('');
}

function AddAppointment() {
    if (AddAppointmentValidation()) {
        var sAppDate = moment($('#dtAddAppt').val(), "DD/MM/YYYY").format('YYYY-MM-DD');
        var sAppStrDate = $('#strtimePicker').val();
        var sAppEndDate = $('#endtimePicker').val();
        var sServicesLit = $('#ddlAddServices').val();

        var sApptObj = {
            ApptDate: sAppDate,
            ApptStartTime: sAppStrDate,
            ApptEndTime: sAppEndDate,
            BranchID: 1,
            OwnerID: $('#ddlAddOwner').val(),
            PetID: $('#ddlAddPet').val(),
            InchargeDoctor: $('#ddlAddDoctor').val(),
            EmailNotify: $('#chkNotify').is(':checked'),
            ServiceList: [sServicesLit]
        };

        $.post("/Appointment/CreateAppointment",
            sApptObj,
        ).done(function (result) {
            if (result.StatusCode == 200) {
                $('#AppointmentModal').modal('hide');
                window.location.reload();
            }
            else {
                if (result.isDoctApptOverlap && !result.isPatientAppOverlap) {
                    $('#lbError').html('@Html.Raw(LangResources["Appointment_Message_DoctorNotAvailable"])');
                    $('#divdtAddAppt').addClass("errorValidation");
                    $('#divtimePicker').addClass("errorValidation");
                }

                if (!result.isDoctApptOverlap && result.isPatientAppOverlap) {
                    $('#lbError').html('@Html.Raw(LangResources["Appointment_Message_SamePatientAppointment"])');
                    $('#divdtAddAppt').addClass("errorValidation");
                    $('#divtimePicker').addClass("errorValidation");
                }

                $('#lbError').css('visibility', 'visible');
            }
        });
    }
    else {
        $('#lbError').html('@Html.Raw(LangResources["Appointment_Label_MandatoryFields"].ToString())');
        $('#lbError').css('visibility', 'visible');
    }
}

function AddAppointmentValidation() {
    $('#lbError').css('visibility', 'hidden');
    let isValid = true;

    // ---- Owner Dropdown ---- //
    if ($('#ddlAddOwner').val() == '') {
        $('#ddlAddOwner').addClass("errorValidation");
        $('#ddlAddOwner').focus();

        isValid = false;
    }
    else {
        $('#ddlAddOwner').removeClass("errorValidation");
    }

    // ---- Pet Dropdown ------ //
    if ($('#ddlAddPet').val() == '') {
        $('#ddlAddPet').addClass("errorValidation");
        $('#ddlAddPet').focus();

        isValid = false;
    }
    else {
        $('#ddlAddPet').removeClass("errorValidation");
    }

    // ----- Doctor Dropdown ----- //
    if ($('#ddlAddDoctor').val() == '') {
        $('#ddlAddDoctor').addClass("errorValidation");
        $('#ddlAddDoctor').focus();

        isValid = false;
    }
    else {
        $('#ddlAddDoctor').removeClass("errorValidation");
    }

    // ----- Services Dropdown ----- //
    if ($('#ddlAddServices').val() == '') {
        $('#ddlAddServices').addClass("errorValidation");
        $('#ddlAddServices').focus();

        isValid = false;
    }
    else {
        $('#ddlAddServices').removeClass("errorValidation");
    }

    // ------ Appointment date ------ //
    if ($('#dtAddAppt').val() == '') {
        $('#divdtAddAppt').addClass("errorValidation");
        $('#divdtAddAppt').focus();

        isValid = false;
    }
    else {
        $('#divdtAddAppt').removeClass("errorValidation");
    }

    // --------- Start Time --------- //
    if ($('#strtimePicker').val() == '') {
        $('#divtimePicker').addClass("errorValidation");
        $('#divtimePicker').focus();

        isValid = false;
    }
    else {
        $('#divtimePicker').removeClass("errorValidation");
    }

    // ---------- End Time ---------- //
    if ($('#endtimePicker').val() == '') {
        $('#divtimePicker').addClass("errorValidation");
        $('#divtimePicker').focus();

        isValid = false;
    }
    else {
        $('#divtimePicker').removeClass("errorValidation");
    }

    return isValid;
}

/*------- Create New client Appointment -------*/
function AddNewAppointment() {
    if (AddNewAppointmentValidation()) {
        var sNewAppDate = moment($('#dtNewApptDate').val(), "DD/MM/YYYY").format('YYYY-MM-DD');
        var sNewPetDOB = moment($('#dtNewDOB').val(), "DD/MM/YYYY").format('YYYY-MM-DD');
        var sNewAppStrDate = $('#newStrtimePicker').val();
        var sNewAppEndDate = $('#newEndtimePicker').val();
        var sNewServicesList = $('#ddlNewAddServices').val();

        var sNewApptObj = {
            ApptDate: sNewAppDate,
            ApptStartTime: sNewAppStrDate,
            ApptEndTime: sNewAppEndDate,
            PetDOB: sNewPetDOB,
            BranchID: 1,
            OwnerName: $('#txtNewOwnerfullName').val(),
            ContactNo: $('#txtNewContactNo').val(),
            PetName: $('#txtNewPetName').val(),
            Species: $('#ddlNewSpecies').val(),
            InchargeDoctor: $('#ddlNewDoctor').val(),
            EmailAddress: $('#txtNewEmail').val(),
            EmailNotify: $('#chkNewNotify').is(':checked'),
            ServiceList: [sNewServicesList]
        };

        $.post("/Appointment/CreateNewClientAppointment",
            sNewApptObj
        ).done(function (result) {
            if (result.StatusCode == 200) {
                $('#AppointmentModal').modal('hide');

                window.location.reload();
            }
            else {
                if (result.isDoctApptOverlap && !result.isPatientAppOverlap) {
                    $('#lbNewError').html('@Html.Raw(LangResources["Appointment_Message_DoctorNotAvailable"])');
                    $('#divNewDateTime').addClass("errorValidation");
                    $('#divNewTimePicker').addClass("errorValidation");
                }

                $('#lbNewError').css('visibility', 'visible');
            }
        });
    }
    else {
        $('#lbNewError').html('@Html.Raw(LangResources["Appointment_Label_MandatoryFields"].ToString())');
        $('#lbNewError').css('visibility', 'visible');
    }

}

function AddNewAppointmentValidation() {
    $('#lbError').css('visibility', 'hidden');
    let isValid = true;

    /*--------- Owner Name ------------*/
    if ($('#txtNewOwnerfullName').val() == '') {
        $('#txtNewOwnerfullName').addClass("errorValidation");
        $('#txtNewOwnerfullName').focus();

        isValid = false;
    }
    else {
        $('#txtNewOwnerfullName').removeClass("errorValidation");
    }

    /*--------- Contact No -------------*/
    if ($('#txtNewContactNo').val() == '') {
        $('#txtNewContactNo').addClass("errorValidation");
        $('#txtNewContactNo').focus();

        isValid = false;
    }
    else {
        $('#txtNewContactNo').removeClass("errorValidation");
    }

    /*---------- Email Address ---------*/
    if ($('#txtNewEmail').val() == '') {
        $('#txtNewEmail').addClass("errorValidation");
        $('#txtNewEmail').focus();

        isValid = false;
    }
    else {
        $('#txtNewEmail').removeClass("errorValidation");
    }

    /*--------- Pet Name ----------------*/
    if ($('#txtNewPetName').val() == '') {
        $('#txtNewPetName').addClass("errorValidation");
        $('#txtNewPetName').focus();

        isValid = false;
    }
    else {
        $('#txtNewPetName').removeClass("errorValidation");
    }

    /*--------- Pet DOB -----------------*/
    if ($('#dtNewDOB').val() == '') {
        $('#divNewDOB').addClass("errorValidation");
        $('#divNewDOB').focus();

        isValid = false;
    }
    else {
        $('#divNewDOB').removeClass("errorValidation");
    }

    /*------- Species Dropdown ----------*/
    if ($('#ddlNewSpecies').val() == '') {
        $('#ddlNewSpecies').addClass("errorValidation");
        $('#ddlNewSpecies').focus();

        isValid = false;
    }
    else {
        $('#ddlNewSpecies').removeClass("errorValidation");
    }

    /*-------- Services Dropdown -------*/
    if ($('#ddlNewAddServices').val() == '') {
        $('#ddlNewAddServices').addClass("errorValidation");
        $('#ddlNewAddServices').focus();

        isValid = false;
    }
    else {
        $('#ddlNewAddServices').removeClass("errorValidation");
    }

    /*-------- Appt Date -------------*/
    if ($('#dtNewApptDate').val() == '') {
        $('#divNewDateTime').addClass("errorValidation");
        $('#divNewDateTime').focus();

        isValid = false;
    }
    else {
        $('#divNewDateTime').removeClass("errorValidation");
    }

    /*-------- Appt Start Time ----------*/
    if ($('#newStrtimePicker').val() == '') {
        $('#divNewTimePicker').addClass("errorValidation");
        $('#divNewTimePicker').focus();

        isValid = false;
    }
    else {
        $('#divNewTimePicker').removeClass("errorValidation");
    }

    /*--------- Appt End time -------------*/
    if ($('#newEndtimePicker').val() == '') {
        $('#divNewTimePicker').addClass("errorValidation");
        $('#divNewTimePicker').focus();

        isValid = false;
    } else {
        $('#divNewTimePicker').removeClass("errorValidation");
    }

    /*--------- Doctor Dropdown ------------*/
    if ($('#ddlNewDoctor').val() == '') {
        $('#ddlNewDoctor').addClass("errorValidation");
        $('#ddlNewDoctor').focus();

        isValid = false;
    } else {
        $('#ddlNewDoctor').removeClass("errorValidation");
    }

    return isValid;
}

function loadAppointmentData(sID) {
    $.getJSON("/Appointment/GetAppointmentByID", {
        ApptID: sID
    })
        .done(function (result) {
            if (result != null) {
                $('#txtOwner').val(result.OwnerName);
                $('#txtPet').val(result.PetName);
                $('#txtServices').val(result.ServiceName);
                $('#txtDoctor').val(result.DoctorName);
                $('#hidEditID').val(result.AppointmentID);

                $('#dtEditAppt').val(moment(result.ApptDate).format("DD-MM-YYYY"));
                $('#EditStrttimePicker').val(moment(result.ApptStartTimeString, 'hh:mm:ss').format('HH:mm'));
                $('#EditEndtimePicker').val(moment(result.ApptEndTimeString, 'hh:mm:ss').format('HH:mm'));
            }
        })
}

function cancelAppointment(AppointmentID, StatusToUpdate) {
    $.post("/Appointment/UpdateAppointmentStatus", {
        ApptID: AppointmentID,
        ApptStatus: StatusToUpdate
    }).done(function (result) {
        return result;
    });
}

$(document).ready(function () {

    $('#btnCreate').on('click', function (ctl) {
        $('#divNewAppt').show();
        $('#divNewClientAppt').hide();
        $('#divEditAppt').hide();

        clearFields();

        $('#AppointmentModal').modal('show');
    });

    $('#btnSearch').on('click', function (ctl) {
        sessionStorage.setItem("CalendarView", "dayGridMonth");
        CalendarViewChanges(calendar, 'dayGridMonth');
    });

    $('#btnReset').on('click', function (ctl) {
        $('#ddlSearchOwner').val($('#ddlSearchOwner option:first').val());

        $('#ddlSearchPet').empty();
        $('#ddlSearchPet').append(new Option('@Html.Raw(LangResources["Appointment_Selection_SelectPet"])', ''));
        $('#ddlSearchPet').val($('#ddlSearchPet option:first').val());

        $('#ddlSearchServices').val($('#ddlSearchServices option:first').val());

        $('#ddlSearchDoctor').empty();
        $('#ddlSearchDoctor').append(new Option('@Html.Raw(LangResources["Appointment_Selection_SelectDoctor"])', ''));
        $('#ddlSearchDoctor').val($('#ddlSearchDoctor option:first').val());
    });

    $('#btnEditAppt').on('click', function (ctl) {
        $('#divNewAppt').hide();
        $('#divNewClientAppt').hide();
        $('#divEditAppt').show();

        var sApptID = $('#hidApptID').val()

        loadAppointmentData(sApptID);

        $('#EventModal').modal('hide');
        $('#AppointmentModal').modal('show');
    });

    $('#btnUpdateAppt').on('click', function (ctl) {
        var sAppEditDate = moment($('#dtEditAppt').val(), "DD/MM/YYYY").format('YYYY-MM-DD');
        var sAppEditStrDate = $('#EditStrttimePicker').val();
        var sAppEditEndDate = $('#EditEndtimePicker').val();
        var sApptEditID = $('#hidEditID').val();

        $.post("/Appointment/UpdateAppointment", {
            ApptDate: sAppEditDate,
            ApptStartTime: sAppEditStrDate,
            ApptEndTime: sAppEditEndDate,
            ApptID: sApptEditID
        }).done(function (result) {
            if (result.StatusCode == 200) {
                $('#lbEditError').html('@Html.Raw(LangResources["Appointment_Message_UpdateSuccessfully"])');
                $('#lbEditError').css('visibility', 'visible');

                setTimeout(function () {
                    $('#AppointmentModal').modal('hide');
                    window.location.reload();
                }, 1500);
            }
            else {
                $('#lbEditError').html('@Html.Raw(LangResources["Appointment_Message_UpdateCancelFailed"])');
                $('#lbEditError').css('visibility', 'visible');
            }
        });
    });

    $('#btnCancelAppt').on('click', function (ctl) {
        var sApptEditID = $('#hidEditID').val();
        var sStatus = 2;

        $.post("/Appointment/UpdateAppointmentStatus", {
            ApptID: sApptEditID,
            ApptStatus: sStatus
        }).done(function (result) {
            if (result.StatusCode == 200) {
                $('#lbEditError').html('@Html.Raw(LangResources["Appointment_Message_ApptCancelUpdated"])');
                $('#lbEditError').css('visibility', 'visible');

                setTimeout(function () {
                    $('#AppointmentModal').modal('hide');

                    window.location.reload();
                }, 2500);
            }
            else {
                $('#lbEditError').html('@Html.Raw(LangResources["Appointment_Message_UpdateCancelFailed"])');
                $('#lbEditError').css('visibility', 'visible');
            }
        });
        // var result = cancelAppointment(sApptEditID, sStatus);
        // if (result.StatusCode == 200) {
        //     $('#lbEditError').html('Appointment Cancelled Updated.');
        //     $('#lbEditError').css('visibility', 'visible');

        //     setTimeout(function () {
        //         $('#AppointmentModal').modal('hide');

        //         window.location.reload();
        //     }, 2500);
        // }
        // else {
        //     $('#lbEditError').html('Failed to update appointment, please try again later.');
        //     $('#lbEditError').css('visibility', 'visible');
        // }
    });

    $('#btnDirectCancel').on('click', function (ctl) {

        ShowConfirmCancelMsgBox("@Html.Raw(LangResources['Appointment_Title_CancelAppointment'])", "@Html.Raw(LangResources['Appointment_Message_ConfirmCancelAppt'])",
            function () {
                var sApptID = $('#hidApptID').val();
                var sStatus = 2;

                $.post("/Appointment/UpdateAppointmentStatus", {
                    ApptID: sApptID,
                    ApptStatus: sStatus
                }).done(function (result) {
                    if (result.StatusCode == 200) {
                        $('#lbEventError').html('@Html.Raw(LangResources["Appointment_Message_ApptCancelUpdated"])');
                        $('#lbEventError').css('visibility', 'visible');

                        setTimeout(function () {
                            $('#EventModal').modal('hide');
                            window.location.reload();
                        }, 2000);
                    }
                    else {
                        $('#lbEventError').html('@Html.Raw(LangResources["Appointment_Message_UpdateCancelFailed"])');
                        $('#lbEventError').css('visibility', 'visible');
                    }
                });
            },
            function () { }
        );
    });
});

function btnTimeGridEditClick() {
    $('#divNewAppt').hide();
    $('#divNewClientAppt').hide();
    $('#divEditAppt').show();

    var sApptID = $('#timeGridApptID').val()
    loadAppointmentData(sApptID);

    $('#AppointmentModal').modal('show');
}

function btnTimeGridCancelClick() {
    ShowConfirmCancelMsgBox("@Html.Raw(LangResources['Appointment_Title_CancelAppointment'])", "@Html.Raw(LangResources['Appointment_Message_ConfirmCancelAppt'])",
        function () {
            var sApptID = $('#timeGridApptID').val();
            var sStatus = 2;

            $.post("/Appointment/UpdateAppointmentStatus", {
                ApptID: sApptID,
                ApptStatus: sStatus
            }).done(function (result) {
                if (result.StatusCode == 200) {
                    //$('#lbEventError').html('Appointment Cancelled Updated.');
                    //$('#lbEventError').css('visibility', 'visible');

                    setTimeout(function () {
                        window.location.reload();
                    }, 2000);
                }
                else {
                    //$('#lbEventError').html('Failed to update appointment, please try again later.');
                    //$('#lbEventError').css('visibility', 'visible');
                }
            });
        },
        function () { }
    );
}

function clearFields() {
    $('#ddlAddOwner').val($('#ddlAddOwner option:first').val());
    $('#ddlAddOwner').removeClass("errorValidation");

    $('#ddlAddPet').empty();
    $('#ddlAddPet').append(new Option('@Html.Raw(LangResources["Appointment_Selection_SelectPet"])', ''));
    $('#ddlAddPet').val($('#ddlAddPet option:first').val());
    $('#ddlAddPet').removeClass("errorValidation");

    $('#ddlAddServices').val($('#ddlAddServices option:first').val());
    $('#ddlAddServices').removeClass("errorValidation");

    $('#ddlAddDoctor').empty();
    $('#ddlAddDoctor').append(new Option('@Html.Raw(LangResources["Appointment_Selection_SelectDoctor"])', ''));
    $('#ddlAddDoctor').val($('#ddlAddDoctor option:first').val());
    $('#ddlAddDoctor').removeClass("errorValidation");

    $('#chkNotify').prop('checked', false);

    $('#dtAddAppt').val('');
    $('#divdtAddAppt').removeClass("errorValidation");

    $('#strtimePicker').val('');
    $('#endtimePicker').val('');
    $('#divtimePicker').removeClass("errorValidation");

    $('#lbError').html('');

    /*------ New Client ------*/
    $('#txtNewOwnerfullName').empty();
    $('#txtNewOwnerfullName').removeClass("errorValidation");

    $('#txtNewContactNo').empty();
    $('#txtNewContactNo').removeClass("errorValidation");

    $('#txtNewEmail').empty();
    $('#txtNewEmail').removeClass("errorValidation");

    $('#txtNewPetName').empty();
    $('#txtNewPetName').removeClass("errorValidation");

    $('#dtNewDOB').empty();
    $('#divNewDOB').removeClass("errorValidation");

    $('#ddlNewSpecies').val($('#ddlNewSpecies option:first').val());
    $('#ddlNewSpecies').removeClass("errorValidation");

    $('#ddlNewAddServices').val($('#ddlNewAddServices option:first').val());
    $('#ddlNewAddServices').removeClass("errorValidation");

    $('#ddlNewDoctor').val($('#ddlNewDoctor option:first').val());
    $('#ddlNewDoctor').removeClass("errorValidation");

    $('#dtNewApptDate').val('');
    $('#divNewDateTime').removeClass("errorValidation");

    $('#newStrtimePicker').val('');
    $('#newEndtimePicker').val('');
    $('#divNewTimePicker').removeClass("errorValidation");

    $('#chkNewNotify').prop('checked', false);

    $('#lbNewError').html('');
}