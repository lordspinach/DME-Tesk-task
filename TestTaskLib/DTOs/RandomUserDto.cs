﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskLib.DTOs
{
    public class RandomUserDto
    {
        public class Response
        {
            [JsonProperty("results")]
            public Result[] Results { get; set; }
            [JsonProperty("info")]
            public Info Info { get; set; }
        }

        public class Info
        {
            [JsonProperty("seed")]
            public string Seed { get; set; }
            [JsonProperty("results")]
            public int Results { get; set; }
            [JsonProperty("page")]
            public int Page { get; set; }
            [JsonProperty("version")]
            public string Version { get; set; }
        }

        public class Result
        {
            [JsonProperty("gender")]
            public string Gender { get; set; }
            [JsonProperty("name")]
            public Name Name { get; set; }
            [JsonProperty("location")]
            public Location Location { get; set; }
            [JsonProperty("email")]
            public string Email { get; set; }
            [JsonProperty("login")]
            public Login Login { get; set; }
            [JsonProperty("dob")]
            public DoB DoB { get; set; }
            [JsonProperty("registered")]
            public Registered Registered { get; set; }
            [JsonProperty("phone")]
            public string Phone { get; set; }
            [JsonProperty("cell")]
            public string Cell { get; set; }
            [JsonProperty("id")]
            public Id Id { get; set; }
            [JsonProperty("picture")]
            public Picture Picture { get; set; }
            [JsonProperty("nat")]
            public string Nat { get; set; }
        }

        public class Name
        {
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("first")]
            public string First { get; set; }
            [JsonProperty("last")]
            public string Last { get; set; }
        }

        public class Location
        {
            [Key]
            public int Id { get; set; }
            [JsonProperty("street")]
            [NotMapped]
            public Street Street { get; set; }
            [JsonProperty("city")]
            public string City { get; set; }
            [JsonProperty("state")]
            public string State { get; set; }
            [JsonProperty("country")]
            public string Coutry { get; set; }
            [JsonProperty("postcode")]
            public string PostCode { get; set; }
            [JsonProperty("Coordinates")]
            [NotMapped]
            public Coordinates Coordinates { get; set; }
            [JsonProperty("timezone")]
            [NotMapped]
            public Timezone Timezone { get; set; }
        }

        public class Login
        {
            [JsonProperty("uuid")]
            public string Uuid { get; set; }
            [JsonProperty("username")]
            public string Username { get; set; }
            [JsonProperty("password")]
            public string Password { get; set; }
            [JsonProperty("salt")]
            public string Salt { get; set; }
            [JsonProperty("md5")]
            public string MD5 { get; set; }
            [JsonProperty("sha1")]
            public string SHA1 { get; set; }
            [JsonProperty("sha256")]
            public string SHA256 { get; set; }
        }

        public class DoB
        {
            [JsonProperty("date")]
            public DateTime Date { get; set; }
            [JsonProperty("age")]
            public int Age { get; set; }
        }

        public class Registered
        {
            [JsonProperty("date")]
            public DateTime Date { get; set; }
            [JsonProperty("age")]
            public int Age { get; set; }
        }

        public class Id
        {
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public class Picture
        {
            [Key]
            public int Id { get; set; }
            [JsonProperty("large")]
            public string Large { get; set; }
            [JsonProperty("medium")]
            public string Medium { get; set; }
            [JsonProperty("thumbnail")]
            public string Thumbnail { get; set; }
        }

        public class Street
        {
            [JsonProperty("number")]
            public int Number { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
        }

        public class Timezone
        {
            [JsonProperty("offset")]
            public string Offset { get; set; }
            [JsonProperty("description")]
            public string Description { get; set; }
        }

        public class Coordinates
        {
            [JsonProperty("latitude")]
            public double Latitude { get; set; }
            [JsonProperty("longtitude")]
            public double Longitude { get; set; }
        }
    }
}
