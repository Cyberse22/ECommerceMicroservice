package com.cynet.productservice.controllers;

import com.cynet.productservice.entities.Gallery;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.env.Environment;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.client.RestTemplate;

import java.util.ArrayList;
import java.util.List;

@RestController
@RequestMapping("/")
public class HomeController {
    @Autowired
    private RestTemplate restTemplate;

    @Autowired
    private Environment env;

    @RequestMapping("/")
    public String home() {
        return "Hello from Gallery Service running at port: " + env.getProperty("local.server.port");
    }

    @RequestMapping("/{id}")
    public Gallery getGallery(@PathVariable final int id) {
        Gallery gallery = new Gallery();
        gallery.setId(id);

        List<Object> images = new ArrayList<>();
        gallery.setImages(images);

        return gallery;
    }

    @RequestMapping("/admin")
    public String homeAdmin() {
        return "This is the admin area of Gallery service running at port: " + env.getProperty("local.server.port");
    }
}
