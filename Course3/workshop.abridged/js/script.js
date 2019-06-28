
var bookDataFromLocalStorage = [];

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
                        BookBoughtDate: { type: "string" }
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
            { field: "BookId", title: "書籍編號",width:"10%"},
            { field: "BookName", title: "書籍名稱", width: "50%" },
            { field: "BookCategory", title: "書籍種類", width: "10%" },
            { field: "BookAuthor", title: "作者", width: "15%" },
            { field: "BookBoughtDate", title: "購買日期", width: "15%" },
            { command: { text: "刪除", click: deleteBook }, title: " ", width: "120px" }
        ]
        
    });

    $(".book-grid-search").bind('input propertychange', function(){
        console.log($(".book-grid-search").val());
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
                }
            ]
        });
    });
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
    for(var i = 0; i < localData.length; i++)
    {
        console.log(localData[i].BookId + ' ' + dataItem.BookId);
        if(localData[i].BookId == dataItem.BookId)
        {
            console.log("find");
            localData.splice(i, 1);
            break;
        }
    }
    localStorage["bookData"] = JSON.stringify(localData);
    location.reload();
};

$("button").click(function(){
    var insert_category = $("#book_category").data("kendoDropDownList").text();
    var insert_name = $("#book_name").val();
    var insert_author = $("#book_author").val();
    var insert_date = $("#bought_datepicker").val();
    var datasource = JSON.parse(localStorage.getItem("bookData"));
    var insert_Id = datasource[datasource.length-1].BookId+1;
    datasource.push({
        BookId: insert_Id,
        BookName: insert_name,
        BookCategory: insert_category,
        BookAuthor: insert_author,
        BookBoughtDate: insert_date
    });
    localStorage.setItem("bookData", JSON.stringify(datasource));
    location.reload();
})



