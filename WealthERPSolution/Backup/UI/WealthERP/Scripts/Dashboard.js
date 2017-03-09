$(document).ready(function() {
    setSortOrder();
    $(".cPanel").hide();
    $(".cLink").click(function() {
        $(this).next(".cPanel").slideToggle(600);
    });
    $('.btnClose').click(function() {
        $(".cPanel").slideUp();
        return false;
    });
    $('.dragbox').each(function() {
        $(this).hover(function() {
            $(this).find('h2').addClass('collapse');
        }, function() {
            $(this).find('h2').removeClass('collapse');
        })
    		.find('h2').hover(function() {
    		    $(this).find('.configure').css('visibility', 'visible');
    		}, function() {
    		    $(this).find('.configure').css('visibility', 'hidden');
    		})
		    .click(function() {
		        $(this).siblings('.dragbox-content').toggle();
		    })
		    .end()
		    .find('.configure').css('visibility', 'hidden');
    });
    $('.column').sortable({
        connectWith: '.column',
        handle: 'h2',
        cursor: 'move',
        placeholder: 'placeholder',
        forcePlaceholderSize: true,
        opacity: 0.4,
        stop: function(event, ui) {
            $(ui.item).find('h2').click();
            ui.item.css({ 'top': '0', 'left': '0' });
            if (setSortOrder()) __doPostBack(SortOrderId, '');
        }
    })
	.disableSelection();
});

function setSortOrder() {
    var hasChanged = false;

    var sortorder = '';
    var obj = document.getElementsByTagName("div");
    for (var i = 0; i < obj.length; i++) {
        if (typeof (obj[i].id) != "undefined" && obj[i].id.indexOf("_Part") != -1) {
            sortorder += "," + obj[i].id.substr(obj[i].id.indexOf("_Part") + 5);
        }
    }
    if (sortorder != '') sortorder = sortorder.substr(1);
    
    if (sortorder != $('#' + SortOrderId).val())
        hasChanged = true;
        
    $('#' + SortOrderId).val(sortorder);

    return hasChanged;
}
