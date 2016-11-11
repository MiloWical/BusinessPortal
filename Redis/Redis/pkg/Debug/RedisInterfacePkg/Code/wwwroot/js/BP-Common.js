function toggleVisibilityById(id) {
    
    var currentElementStyle =  document.getElementById(id).style;

    currentElementStyle.display = 
        (currentElementStyle.display == 'block') ? 'none' : 'block';
}

function showById(id) {
    document
        .getElementById(id)
        .style
        .display 
            = 'block';
}

function hideById(id) {
    document
        .getElementById(id)
        .style
        .display 
            = 'none';
}