

window.onload = function() {
    

    document.querySelectorAll(".exchanges-sec").forEach(function(exchangeSec) {
        var Cards = Array.from(exchangeSec.querySelectorAll(".cards .card"));
        var top5Cards = Cards
            .map(function(card) {
                return {
                    card: card,
                    count: parseFloat(card.querySelector(".count span").innerHTML.replace(",", ".").replace("%", ""))
                };
            })
            .sort(function(a, b) {
                return b.count - a.count;
            })
            .slice(0, 5);

        var TOP5TL = gsap.timeline({
            repeat: -1,
            yoyo: true,
        });
        console.log(top5Cards);
        top5Cards.forEach(function(card) {

            var firstColor = "#55B024";
            var secondColor = "#1345CC";
            if (card === top5Cards[0]) {
                /*first color red */
                firstColor = "#FF0000";
            }
            else if (card === top5Cards[1]) {
                /*first color yellow */
                firstColor = "#FFD700";
            }



            TOP5TL.to(card.card.querySelector(".count span"), {
                color: firstColor,
                duration: 1
                
            }, "<");

            TOP5TL.to(card.card.querySelector(".count span"), {
                color: secondColor,
                duration: 1
            }, "<");
            

        });


        var columnedCards = [];
        Cards.forEach(function(card) {
            var top = card.getBoundingClientRect().top;
            var column = columnedCards.find(function(column) {
                return column[0].getBoundingClientRect().top === top;
            });

            if (!column) {
                columnedCards.push([card]);
            }
            else {
                column.push(card);
            }
        });
        
        columnedCards.forEach(function(column) {
            gsap.from(column, {
                scrollTrigger: {
                    trigger: column[0],
                    start: "top 140%",
                    end: "bottom 40%",
                    scrub: 0.5,
                    toggleActions: "play none none none",
                    markers: false
                },
                y: 150,
                opacity: 0,
                stagger: 0.1
            });
        });
    });


    document.querySelectorAll(".features-sec").forEach(function(featureSec) {

        /* pinned tl */
        const TL = gsap.timeline({
            scrollTrigger: {
                trigger: featureSec,
                start: "top 0%",
                end: "+=150% 0%",
                scrub: 0.5,
                pin: true,
                markers: false
            }
        });

        var bg = featureSec.querySelector(".bg");

        
        var arrowColumns = Array.from(bg.querySelectorAll(".arrows"));
        var arrows = Array.from(bg.querySelectorAll(".arrows .arrow"));
        var arrowSVGPAths = Array.from(bg.querySelectorAll(".arrows .arrow svg path"));

        arrowSVGPAths.forEach(function(path) {
            path.style.strokeDasharray = path.getTotalLength();
            path.style.strokeDashoffset = path.getTotalLength();
        });

        TL.to(arrowColumns.reverse(), {
            y: -500,
            stagger: 0.1
        });

        var sloganText = featureSec.querySelector(".slogan-text");
        
        TL.to(sloganText, {
            y: -700,
            duration: 0.8
        }, "<");

        arrowColumns.forEach(function(column) {
            var arrowPaths = Array.from(column.querySelectorAll(".arrow svg path"));

            TL.to(arrowPaths, {
                fill: "#1345CC",
                duration: 0.5,
                stagger: 0.05
            }, "<");
        });

        

        TL.to(arrowColumns[2].querySelectorAll('.arrow')[2], {
            scale: 30,
            duration: 0.5,
            transformOrigin: "center",
        }, "-=0.6");
        

        var sloganSec2 = document.querySelector(".features-sec .slogan-text2");
        gsap.set(sloganSec2, {
            y: 200
        });

        var centerPath = arrowColumns[2].querySelectorAll('.arrow')[2].querySelector("svg path")
        TL.to(centerPath, {
            fill: "#003c66",
            duration: 0.5,
        }, "-=0.6");

        TL.to(arrowSVGPAths.filter(function(path) {
            return path !== centerPath;
        }), {
            fill: "#55B024",
            duration: 0.5
        }, "<");

        TL.to(sloganSec2, {
            y: 0,
            opacity: 1,
            duration: 0.8
        }, "-=0.5");

    });



    document.querySelectorAll(".faqs-sec").forEach(function(faqSec) {
        var faqs = faqSec.querySelectorAll(".faq");

        faqs.forEach(function(faq) {
            
            faq.addEventListener("click", function() {

                faqs.forEach(function(faq) {
                    var answer = faq.querySelector(".answer");
                    gsap.to(answer, {
                        maxHeight: 0,
                        duration: 0.3,
                        ease: "power2.out"
                    });
                    faq.classList.remove("active");
                });
                
                faq.classList.add("active");

                gsap.to(faq.querySelector(".answer"), {
                    maxHeight: faq.querySelector(".answer").scrollHeight,
                    duration: 0.3,
                    ease: "power2.inOut"
                });
                


            });
        });
    });

    document.querySelectorAll(".video-sec").forEach(function(videoSec) {
        var video = videoSec.querySelector("video");
        var videoDiv = videoSec.querySelector(".video");
        /* pinned TL */

        /*-
        const TL = gsap.timeline({
            scrollTrigger: {
                trigger: videoSec,
                start: "top 0%",
                end: "+=150% 0%",
                scrub: 0.5,
                pin: true,
                markers: false
            }
        });
        */

        var pause = videoSec.querySelector(".pause");
        var mute = videoSec.querySelector(".mute");
        var fullscreen = videoSec.querySelector(".fullscreen");

        pause.addEventListener("click", function() {
            if (video.paused) {
                video.play();
                pause.classList.add("playing");
                pause.innerHTML = '<i class="fas fa-pause"></i>';
            }
            else {
                video.pause();
                pause.classList.remove("playing");
                pause.innerHTML = '<i class="fas fa-play"></i>';
            }
        });

        mute.addEventListener("click", function() {
            if (video.muted) {
                video.muted = false;
                mute.innerHTML = '<i class="fas fa-volume-up"></i>';
            }
            else {
                video.muted = true;
                mute.innerHTML = '<i class="fas fa-volume-mute"></i>';
            }
        });

        fullscreen.addEventListener("click", function() {
            
            if (video.requestFullscreen) {
                video.requestFullscreen();
            } else if (video.mozRequestFullScreen) { /* Firefox */
                video.mozRequestFullScreen();
            } else if (video.webkitRequestFullscreen) { /* Chrome, Safari and Opera */
                video.webkitRequestFullscreen();
            } else if (video.msRequestFullscreen) { /* IE/Edge */
                video.msRequestFullscreen();
            }
        });

        /* videonun kontrollerinden mute değiştiğinde butonun durumunu güncelle */
        video.addEventListener("volumechange", function() {
            if (video.muted) {
                mute.innerHTML = '<i class="fas fa-volume-mute"></i>';
            }
            else {
                mute.innerHTML = '<i class="fas fa-volume-up"></i>';
            }
        });

        /* videonun durumu değiştiğinde butonun durumunu güncelle */
        video.addEventListener("play", function() {
            pause.classList.add("playing");
            pause.innerHTML = '<i class="fas fa-pause"></i>';
        });

        video.addEventListener("pause", function() {
            pause.classList.remove("playing");
            pause.innerHTML = '<i class="fas fa-play"></i>';
        });

        /* videonun fullscreen durumunu değiştiğinde butonun durumunu güncelle */
        document.addEventListener("fullscreenchange", function() {
            if (document.fullscreenElement) {
                fullscreen.innerHTML = '<i class="fas fa-compress"></i>';
            }
            else {
                fullscreen.innerHTML = '<i class="fas fa-expand"></i>';
            }
        });
    
    });

}

