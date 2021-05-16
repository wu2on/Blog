let select = document.getElementsByClassName("custom-blog_text");

let tags = select.length == 0 ? 0 : select[0].innerHTML.match(/#[a-z]+/gi);



if (tags.length >= 1) {
    let innerHTML = select[0].innerHTML;

    for (let i = 0; i < tags.length; i += 1) {
        innerHTML = innerHTML.substring(0, innerHTML.indexOf(tags[i])) + "<span class='tag'>" + innerHTML.substring(innerHTML.indexOf(tags[i]), innerHTML.indexOf(tags[i]) + tags[i].length) + "</span>" + innerHTML.substring(innerHTML.indexOf(tags[i]) + tags[i].length);
    }

    select[0].innerHTML = innerHTML;
}



