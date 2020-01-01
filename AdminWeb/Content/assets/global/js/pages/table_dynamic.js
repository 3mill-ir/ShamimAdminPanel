$(function () {

    function fnFormatDetails(oTable, nTr) {
        var aData = oTable.fnGetData(nTr);
        var sOut = "";
         var sOut = '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;width:100%;">';
         sOut += '<tr style="float: right;width:100%;"><td style="width: 90px;">متن نظر : </td><td style="width: 90%;padding-left: 30px;text-align: justify;">' + aData[1] + '</td></tr>';
         sOut += '<tr style="margin-top: 20px;float: right;width: 100%;"><td style="width: 90px;">عنوان پست :  </td><td style="width: 90%;padding-left: 30px;text-align: justify;">' + aData[2] + '</td></tr>';
        sOut += '</table>';
        return sOut;
    }

    /*  Insert a 'details' column to the table  */
    var nCloneTh = document.createElement('th');
    var nCloneTd = document.createElement('td');
    nCloneTd.innerHTML = '<i class="fa fa-plus-square-o"></i>';
    nCloneTd.className = "center";


    $('#table2 thead tr').each(function () {
        this.insertBefore(nCloneTh, this.childNodes[0]);
    });

    $('#table2 tbody tr').each(function () {
         this.insertBefore(nCloneTd.cloneNode(true), this.childNodes[0]);
    });

    /*  Initialse DataTables, with no sorting on the 'details' column  */
    var oTable = $('#table2').dataTable({
        "aoColumnDefs": [{
            "bSortable": false,
            "aTargets": [0]
        }],
        "aaSorting": [
            [1, 'asc']
        ]
    });

    /*  Add event listener for opening and closing details  */
    $(document).on('click', '#table2 tbody td i', function () {
        var nTr = $(this).parents('tr')[0];
        if (oTable.fnIsOpen(nTr)) {
            /* This row is already open - close it */
            $(this).removeClass().addClass('fa fa-plus-square-o');
            oTable.fnClose(nTr);
        } else {
            /* Open this row */
            $(this).removeClass().addClass('fa fa-minus-square-o');
            oTable.fnOpen(nTr, fnFormatDetails(oTable, nTr), 'details');
        }
    });

});