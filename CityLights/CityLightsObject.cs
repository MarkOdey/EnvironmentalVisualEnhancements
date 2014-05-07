﻿
using EveManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;

namespace CityLights
{
    public class CityLightsMaterial : MaterialManager
    {
        [Persistent]
        String _DarkOverlayTex = "";
        [Persistent]
        String _DarkOverlayDetailTex = "";
        [Persistent]
        float _DarkOverlayDetailScale = 80f;
    }

    public class CityLightsObject : IEVEObject
    {
        public String Name { get { return body; } }
        public ConfigNode ConfigNode { get { return node; } }
        public String Body { get { return body; } }
        private String body;
        private ConfigNode node;
        [Persistent]
        CityLightsMaterial cityLightsMaterial = null;

        public void LoadConfigNode(ConfigNode node, String body)
        {
            ConfigNode.LoadObjectFromConfig(this, node);
            this.node = node;
            this.body = body;
        }
        public ConfigNode GetConfigNode()
        {
            return ConfigNode.CreateConfigFromObject(this, new ConfigNode(body));
        }

        public void Apply()
        {
            CelestialBody celestialBody = EVEManagerClass.GetCelestialBody(body);
            if (celestialBody != null)
            {
                celestialBody.pqsController.surfaceMaterial.EnableKeyword("CITYOVERLAY_ON");
                cityLightsMaterial.ApplyMaterialProperties(celestialBody.pqsController.surfaceMaterial);
            }
        }

        public void Remove()
        {
            CelestialBody celestialBody = EVEManagerClass.GetCelestialBody(body);
            if (celestialBody != null)
            {
                celestialBody.pqsController.surfaceMaterial.DisableKeyword("CITYOVERLAY_ON");
            }
        }
    }

}
