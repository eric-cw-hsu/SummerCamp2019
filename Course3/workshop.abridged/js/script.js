//declaration
var bookDataFromLocalStorage = [];
var validator;

$(function(){
    loadBookData();
    var data = [
        {text:"資料庫",value:"database"},
        {text:"網際網路",value:"internet"},
        {text:"應用系統整合",value:"system"},
        {text:"家庭保健",value:"home"},
        {text:"語言",value:"language"},
        {text:"行銷",value:"marketing"},
        {text:"管理",value:"manage"}
    ];

    $("#book_category").kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",
        dataSource: data,
        index: 0,
        change: onChange
    });
    $("#bought_datepicker").kendoDatePicker({
        value:new Date(),
        format: "yyyy-MM-dd"
    });

    $("#book_grid").kendoGrid({
        dataSource: {
            data: bookDataFromLocalStorage,
            schema: {
                model: {
                    fields: {
                        BookId: {type:"int"},
                        BookName: { type: "string" },
                        BookCategory: { type: "string" },
                        BookAuthor: { type: "string" },
                        BookBoughtDate: { type: "string" },
                        BookPublisher: {type: "string" }
                    }
                }
            },
            pageSize: 20,
        },
        toolbar: kendo.template("<div class='book-grid-toolbar'><input class='book-grid-search' placeholder='我想要找......' type='text'></input></div>"),
        height: 550,
        sortable: true,
        pageable: {
            input: true,
            numeric: false
        },
        columns: [
            { field: "BookId", title: "書籍編號",width:"9%"},
            { field: "BookName", title: "書籍名稱", width: "39%" },
            { field: "BookCategory", title: "書籍種類", width: "10%" },
            { field: "BookAuthor", title: "作者", width: "15%" },
            { field: "BookBoughtDate", title: "購買日期", width: "12%" },
            { field: "BookPublisher", title: "出版社", width: "15%"},
            { command: { text: "刪除", click: deleteBook }, title: " ", width: "120px" }
        ]
        
    });

    $(".book-grid-search").bind('input propertychange', function(){
        var target = $(".book-grid-search").val();
        $("#book_grid").data("kendoGrid").dataSource.filter({
            logic: "or",
            filters:[
                {
                    field: "BookName",
                    operator: "contains",
                    value: target
                },
                {
                    field: "BookAuthor",
                    operator: "contains",
                    value: target
                },
                {
                    field: "BookPublisher",
                    operator: "contains",
                    value: target
                },
            ]
        });
    });

    validator = $("#window").kendoValidator().data("kendoValidator");
    status = $(".status");
})

function loadBookData(){
    bookDataFromLocalStorage = JSON.parse(localStorage.getItem("bookData"));
    if(bookDataFromLocalStorage == null){
        bookDataFromLocalStorage = bookData;
        localStorage.setItem("bookData",JSON.stringify(bookDataFromLocalStorage));
    }
}

function onChange(){
    var category = $("#book_category").val();
    $(".book-image").attr("src", "image/"+category+".jpg");
};
  
function deleteBook(option){
    var grid = $("#book_grid").data("kendoGrid");
    var dataItem = grid.dataItem($(option.currentTarget).closest("tr"));  
    var localData = JSON.parse(localStorage['bookData']);
    var target = dataItem.BookId;
    //binary search
    var left = 0;
    var right = localData.length-1;
    var middle = 0;
    while(left <= right)
    {
        middle = Math.floor((left+right)/2);
        if(localData[middle].BookId > target)
            right = middle-1;
        else if(localData[middle].BookId < target)
            left = middle+1;
        else if(localData[middle].BookId == target)
            break;
    }
    localData.splice(middle, 1);
    //end binary search
    localStorage["bookData"] = JSON.stringify(localData);
    grid.dataSource.remove(dataItem);
};

$("#add_book").click(function(){
    if(validator.validate())
    {
        var localData = JSON.parse(localStorage.getItem("bookData"));
        var new_book = {};
        
        new_book.BookId = localData[localData.length-1].BookId+1;
        new_book.BookCategory = $("#book_category").data("kendoDropDownList").text();
        new_book.BookName = $("#book_name").val();
        new_book.BookAuthor = $("#book_author").val();
        new_book.BookBoughtDate = $("#bought_datepicker").val();
        new_book.BookPublisher = $("#book_publisher").val();
        //ipush data in the local and the table
        var datasource = JSON.parse(localStorage.getItem("bookData"));
        datasource.push(new_book);
        var grid = $("#book_grid").data("kendoGrid");
        grid.dataSource.add(new_book);
        localStorage.setItem("bookData", JSON.stringify(datasource));   
        //initialization
        $("#book_name").val('');
        $("#book_author").val('');
        $("#book_publisher").val('');
        $("#window").data("kendoWindow").close();
    }
    else
    {
        $(".status").removeClass("valid")
                    .addClass("invalid");
    }
})

$(document).ready(function() {
    var myWindow = $("#window"),
        undo = $("#undo");

    undo.click(function() {
        myWindow.data("kendoWindow").center().open();
        //undo.fadeOut();
    });

    function onClose() {
        undo.fadeIn();
    }

    myWindow.kendoWindow({
        width: "600px",
        title: "新增書籍",
        visible: false,
        actions: [
            "Pin",
            "Minimize",
            "Maximize",
            "Close"
        ],
        close: onClose
    })
});



