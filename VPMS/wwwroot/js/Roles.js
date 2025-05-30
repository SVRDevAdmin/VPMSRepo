$("#SubMenuAccess").toggleClass('show');
document.querySelector("#SubMenuAccessParent").querySelector("#imgArrow").classList.toggle("Up");

var paginationLimit = 5;
var pageSize = 10;
var pageIndex = 1;
var currentPage = 1;
var totalPagination = 0;
var startPagination = 1;
var endPagination = 0;

$(document).ready(function () {
    loadRoleListing();
});

function loadRoleListing() {
    $('#lbRoleSettingError').css('visibility', 'hidden');

    $.getJSON("/Roles/GetRoleListing", {
        //organizationid: '@sessionOrganizationID',
        organizationid: sessionOrgID,
        pageSize: pageSize,
        pageIndex: currentPage
    }).done(function (result) {
        if (result.data != null && result.data.length > 0) {
            //$('#tblRoles').bootstrapTable('destroy');
            //$('#tblRoles').bootstrapTable({
            //    data: result.data
            //});
            debugger;
            var sRowHtml = '';
            for (i = 0; i < result.data.length; i++) {
                sRowHtml += '<tr>' +
                    '<td scope="col" class="text-center">' + result.data[i].SeqNo + '</td>' +
                    '<td scope="col">' + result.data[i].RoleName + '</td>' +
                    '<td scope="col">' + result.data[i].TotalAssigned + '</td>' +
                    '<td scope="col">' + result.data[i].TotalPermissions + '</td>' +
                    '<td scope="col">' +
                    '<button class="btn btn-outline-primary" style="font-size: 12px;" onclick="showPermission(\'' + result.data[i].sPermission + '\')">' +
                    langRes["Roles_Button_PermissionDetails"] +
                    '</button>' +
                    '</td>' +
                    '<td scope="col">' + populateHideMenu(result.data[i].RoleID) + '</td>' +
                    '</tr>';
            }
            $('#tblRoles tbody').append(sRowHtml);

            /*------- Pagination -------*/
            populatePagingControl(result.totalRecord);
        }
        else {
            $('#tblRoles').bootstrapTable('destroy');
            document.getElementById("pagination").hidden = true;
        }

    });
}

function populateHideMenu(id) {
    var hideMenuHtml = '<button type="button" class="btn" data-toggle="dropdown">' +
        '<img class="roleListingMoreIcon"  />' +
        '</button>' +

        '<div class="dropdown-menu hiddenMenuStyle containerBackground5">' +
        '<a class="dropdown-item" href="/Roles/ViewRoleProfile/' + id + '/View">' + '@Html.Raw(LangResources["Roles_Menu_View"])' + '</a>' +
        '<a class="dropdown-item" href="/Roles/ViewRoleProfile/' + id + '/Edit">' + '@Html.Raw(LangResources["Roles_Menu_Edit"])' + '</a>' +
        '<a class="dropdown-item" onclick="mnDeleteOnClick(\'' + id + '\')">' + '@Html.Raw(LangResources["Roles_Menu_Delete"])' + '</a>' +
        '</div>';
    return hideMenuHtml;
}

function showPermission(permissions) {
    $('#lstPermission').html('');

    var sPermissionHtml = permissions.trim().split(",");
    if (sPermissionHtml.length > 0) {
        var sRowHtml = '';
        for (i = 0; i < sPermissionHtml.length; i++) {
            sRowHtml += sPermissionHtml[i] + '<br />';
        }
        $('#lstPermission').append(sRowHtml);
    }

    $('#modalPermissionViews').modal('show');
}

function populatePagingControl(totalRecord) {
    let total = totalRecord;
    if (total == 0) {
        document.getElementById("pagination").hidden = true;
    }
    else {
        document.getElementById("pagination").hidden = false;

        var totalPage = parseInt(total / pageSize);
        if (totalPage == 0) {
            totalPage = totalPage + 1;
        }

        if ((total > pageSize) && (total % pageSize) != 0) {
            totalPage = totalPage + 1;
        }

        totalPagination = totalPage;

        if (totalPage > paginationLimit) {
            endPagination = paginationLimit
        }
        else {
            endPagination = totalPage;
        }

        pagination(startPagination, endPagination);
    }

    if (currentPage == totalPagination) {
        document.getElementById("nextButton").hidden = true;
    }
    else {
        document.getElementById("nextButton").hidden = false;
    }
    if (currentPage == 1) {
        document.getElementById("prevButton").hidden = true;
    }
    else {
        document.getElementById("prevButton").hidden = false;
    }
}

function pagination(start, end) {
    var element = document.getElementById("paginationNumbering");

    element.innerHTML = "";
    for (let i = start; i < end + 1; i++) {
        var buttonClassName = "PaginationButton";

        if (currentPage == i) {
            buttonClassName = "PaginationButtonSelected";
        }

        element.innerHTML = element.innerHTML +
            '<div>' +
            '<button onclick="changePage(this);" data-page="' + i + '" class="' + buttonClassName + '"> ' + ('0' + i).slice(-2) + ' </button>' +
            '</div>';
    }
}

function prev() {
    currentPage = currentPage - 1;
    loadRoleListing();
}

function next() {
    currentPage = currentPage + 1;

    loadRoleListing();
}

function changePage(page) {
    let pageNum = page.getAttribute("data-page");

    currentPage = Number(pageNum);
    loadRoleListing();
}

function mnDeleteOnClick(id) {
    ShowConfirmCancelMsgBox('@Html.Raw(LangResources["Roles_Title_DeleteRole"])',
        '@Html.Raw(LangResources["Roles_Label_ConfirmDeleteRole"])',
        function () {
            $.post("/Roles/DeleteRole", {
                roleID: id,
                userID: '@sessionUserID'
            }).done(function (result) {
                if (result.StatusCode == 200) {
                    window.location.reload();
                }
                else {
                    $('#lbRoleSettingError').html('@Html.Raw(LangResources["Roles_Label_FailedToDeleteRecord"])');
                    $('#lbRoleSettingError').css('visibility', 'visible');
                }
            });
        },
        function () { }
    );
}