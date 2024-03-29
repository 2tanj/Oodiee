using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Unity.Services.CloudSave;
using UnityEngine;
using Newtonsoft.Json;

public static class CloudSave
{
    private static readonly ICloudSaveDataClient _client =
        CloudSaveService.Instance.Data;

    public static async Task SaveDataAsync(string key, object value)
    {
        var data = new Dictionary<string, object> { { key, value } };

        try
        {
            await _client.ForceSaveAsync(data);

            Debug.Log("Successful data save.");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    // this returns a PlayerData object
    public static async Task LoadPlayerData()
    {
        try
        {
            //JsonConvert.DeserializeObject<T>(input)

            var data = await _client.LoadAllAsync();
            Debug.Log("Data successfuly loaded.");

            data.ToList()
                .ForEach(pair => {
                    Debug.Log($"{pair.Key} -- {pair.Value}");
                });


            //var cao = JsonConvert.DeserializeObject(
            //            data.Where(pair => pair.Key == "maps")
            //                .Select(pair => pair.Value).First());

            //Debug.Log("custom object: " + cao);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

}