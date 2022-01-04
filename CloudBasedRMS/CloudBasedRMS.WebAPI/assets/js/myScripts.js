var Categories = []
//fetch categories from database
function LoadCategory(element) {
    if (Categories.length == 0) {
        //ajax function for fetch data
        $.ajax({
            type: "GET",
            url: '/Order/getCategories',
            success: function (data) {
                Categories = data;
                //render catagory
                renderCategory(element);
            },
            error: function (error) {
                console.log(error);
            }
        })
    }
    else {
        //render catagory to the element
        renderCategory(element);
    }
}
function renderCategory(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(Categories, function (i, val) {
        $ele.append($('<option/>').val(val.CategoryID).text(val.Description));
    })
}
//fetch Food Items
function LoadFoodItems(categoryDD) {
    $.ajax({
        type: "GET",
        url: "/Order/getFoodItems_Details",
        data: { 'CategoryID': $(categoryDD).val() },
        success: function (data) {
            //render products to appropriate dropdown
            renderFoodItems($(categoryDD).parents('.mycontainer').find('select.fooditems'), data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function renderFoodItems(element, data) {
    //render product
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(data, function (i, val) {
        $ele.append($('<option/>').val(val.FoodItemsID).text(val.Code));
    })
}
//fetch table
var tables = []
function LoadTable(element) {
    if (tables.length == 0) {
        //ajax function for fetch data
        $.ajax({
            type: "GET",
            url: '/Order/getTables',
            success: function (data) {
                tables = data;
                //render catagory
                renderTable(element);
            },
            error: function (error) {
                console.log(error);
            }
        })
    } else {
        //render catagory to the element
        renderTable(element);
    }
}
function renderTable(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(tables, function (i, val) {
        $ele.append($('<option/>').val(val.TableID).text(val.TableNo));
    })
}
$(document).ready(function () {
    //Add button click event
    $('#add').click(function () {
        //validation and add order items
        var isAllValid = true;
        if ($('#Category').val() == "0") {
            isAllValid = false;
            $('#Category').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Category').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#fooditems').val() == "0") {
            isAllValid = false;
            $('#fooditems').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#fooditems').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#quantity').val().trim() != '' && (parseInt($('#quantity').val()) || 0))) {
            isAllValid = false;
            $('#quantity').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#quantity').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#rate').val().trim() != '' && !isNaN($('#rate').val().trim()))) {
            isAllValid = false;
            $('#rate').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#rate').siblings('span.error').css('visibility', 'hidden');
        }

        if (isAllValid) {
            var $newRow = $('#mainrow').clone().removeAttr('id');
            $('.pc', $newRow).val($('#Category').val());
            $('.fooditems', $newRow).val($('#fooditems').val());

            //Replace add button with remove button
            $('#add', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#Category,#fooditems,#quantity,#rate,#add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            //append clone row
            $('#orderdetailsItems').append($newRow);

            //clear select data
            $('#Category,#fooditems').val('0');
            $('#quantity,#rate').val('');
            $('#orderItemError').empty();
        }

    })

    //remove button click event
    $('#orderdetailsItems').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });

    $('#submit').click(function () {
        var isAllValid = true;

        //validate order items
        $('#orderItemError').text('');
        var list = [];
        var errorItemCount = 0;
        $('#orderdetailsItems tbody tr').each(function (index, ele) {
            if (
                $('select.fooditems', this).val() == "0" ||
                (parseInt($('.quantity', this).val()) || 0) == 0 ||
                $('.rate', this).val() == "" ||
                isNaN($('.rate', this).val())
                ) {
                errorItemCount++;
                $(this).addClass('error');
            } else {
                var orderItem = {
                    ProductID: $('select.fooditems', this).val(),
                    Quantity: parseInt($('.quantity', this).val()),
                    Rate: parseFloat($('.rate', this).val())
                }
                list.push(orderItem);
            }
        })

        if (errorItemCount > 0) {
            $('#orderItemError').text(errorItemCount + " invalid entry in order item list.");
            isAllValid = false;
        }

        if (list.length == 0) {
            $('#orderItemError').text('At least 1 order item required.');
            isAllValid = false;
        }

        if ($('#orderNo').val().trim() == '') {
            $('#orderNo').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#orderNo').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#orderDate').val().trim() == '') {
            $('#orderDate').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#orderDate').siblings('span.error').css('visibility', 'hidden');
        }

        if (isAllValid) {
            var data = {
                OrderNo: $('#orderNo').val().trim(),
                OrderDateString: $('#orderDate').val().trim(),
                Description: $('#description').val().trim(),
                OrderDetails: list
            }

            $(this).val('Please wait...');

            $.ajax({
                type: 'POST',
                url: '/home/save',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (data) {
                    if (data.status) {
                        alert('Successfully saved');
                        //here we will clear the form
                        list = [];
                        $('#orderNo,#orderDate,#description').val('');
                        $('#orderdetailsItems').empty();
                    }
                    else {
                        alert('Error');
                    }
                    $('#submit').text('Save');
                },
                error: function (error) {
                    console.log(error);
                    $('#submit').text('Save');
                }
            });
        }
    });
});
LoadCategory($('#Category'));
LoadTable($('#table'))
