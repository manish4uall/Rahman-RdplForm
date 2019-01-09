GetCartItems().done(function (data) {
    console.log('cartitems',data)
    SetCartItemCount(data);
});
  

function GetCartItems() {
    return $.ajax({
        url: "/ShoppingSite/GetCartItems",
        data: JSON.stringify("1"),
        type: "POST",
        dataType: "json",
        contentType: "application/json",

        success: function (data) {
            console.log("GetCartItems success =", data);
            SetCartItemCount(data);
        },
        error: function (data) {
            console.log("GetCartItems error =", data);
        }
    });
}

function addtocart(id) {
    var cart = {
        id: id
    }
    $.ajax({
        type: "POST",
        data: JSON.stringify(cart),
        contentType: 'application/json',
        dataType: 'json',
        url: 'Addtocart',
        success: function (data) {
            SetCartItemCount(data);
            console.log('success', data);
            $("#cartItemCount").html('');
            $("#spcart").html(data.totalCount)
        },
        error: function (data) {
            console.log('err', data);
        }
    });
}

function additemincart(id) {
    var cart = {
        id: id
    }
    $.ajax({
        type: "POST",
        data: JSON.stringify(cart),
        contentType: 'application/json',
        dataType: 'json',
        url: 'Addtocart',
        success: function (data) {
            console.log('success', data)
            $("#txtPlus_" + id).val(data.currentProductCount)
            location.reload();
        },
        error: function (data) {
            console.log('err', data);
        }
    });
}
function SubItemInCart(id) {
    var cart = {
        id: id
    }
    $.ajax({
        type: "POST",
        data: JSON.stringify(cart),
        contentType: 'application/json',
        dataType: 'json',
        url: 'SubItemToCart',
        success: function (data) {
            console.log('success', data)
            $("#txtPlus_" + id).val(data.currentProductCount)
            location.reload();
        },
        error: function (data) {
            console.log('err', data);
        }
    });
}


function SetCartItemCount(data) {
    console.log("SetCartItemCount data =", data);
    $("#cartItemCount").html('');
    $("#spcart").html(data.cartItemsCount);
}

function RemoveFromCart(id) {
    AfterRemove(id);
    console.log("RemoveFromCart id =", id);
    var obj = { id: id };
    $.ajax({
        url: "/ShoppingSite/RemoveFromCart",
        data: JSON.stringify(obj),
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            console.log("RemoveFromCart success =", data);
            //SetCartItemCount(data.cartItemsCount);
            location.reload();
        },
        error: function (data) {
            console.log("AddToCart error =", data);
        }
    });
}
function AfterRemove(id) {
    console.log("id", id)
    $(".slide_" + id).slideUp(800);
}

function Shipping() {
    $.ajax({
        url: "/ShoppingSite/Shipping",
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            console.log("success =", data);
            var r = "";
            var totalPrice = 0;
            $.each(data, function (i, item) {
                r = r + '<p style="color: black; ">' + item.CompanyName + 'x' + item.Quantity + '=' + item.Total + '</p>';
                totalPrice += item.Price * item.Quantity;
            });
            $("#dvTotalView").html(r);
            $("#dvtotal").html("<b>Total Amount</b> =" + totalPrice);
        },
        error: function (data) {
            console.log("AddToCart error =", data);
        }
    });
}



