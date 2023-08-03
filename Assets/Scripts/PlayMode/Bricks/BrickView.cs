using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace PlayMode.Bricks
{
    public class BrickView : MonoBehaviour
    {
        private Brick _brick;
        private CoordinateConverter _converter;
        private BrickData _data;
        private Material _defaultMaterial;
        private Material _lavaMaterial;
        private Material _iceMaterial;
        private Material _stoneMaterial;
        private Material _obsidianMaterial;

        public BrickView Init(Brick brick, CoordinateConverter converter, BrickData data)
        {
            _brick = brick;
            _converter = converter;
            _data = data;

            _brick.OnMovedEvent += MoveView;
            _brick.OnResetedEvent += ResetView;

            LoadMaterials();

            return this;
        }

        private void MoveView()
        {
            for(int i = 0; i < _brick.Shape.Count; i++)
            {
                _data.Shape[i].GameObject.transform.position = _converter.MapCoordinatesToWorld(_brick.Shape[i].Coordinates);
            }
        }

        private void ResetView(Color color)
        {
            _data.BlockPool.HideAllElements();
            foreach (var block in _data.Shape)
            {
                block.GameObject = _data.BlockPool.GetElement().gameObject;
                block.GameObject.transform.position = _converter.MapCoordinatesToWorld(block.Coordinates);

                CheckType(block, color);
            }
        }

        private void CheckType(BrickPart block, Color color)
        {

            switch (block.Type)
            {
                case BlockType.Ice:
                    block.Renderer.material = _iceMaterial;
                    break;
                case BlockType.Stone:
                    block.Renderer.material = _stoneMaterial;
                    break;
                case BlockType.Lava:
                    block.Renderer.material = _lavaMaterial;
                    break;
                case BlockType.Obsidian:
                    block.Renderer.material = _obsidianMaterial;
                    break;
                default:
                    block.Renderer.material = _defaultMaterial;
                    block.Renderer.material.color = color;
                    break;
            }
        }

        private async void LoadMaterials()
        {
            var handle = Addressables.LoadAssetAsync<Material>("Default_Block_Material");
            await handle.Task;
            _defaultMaterial =  handle.Result;
            Addressables.Release(handle);

            handle = Addressables.LoadAssetAsync<Material>("Lava_Block_Material");
            await handle.Task;
            _lavaMaterial = handle.Result;
            Addressables.Release(handle);

            handle = Addressables.LoadAssetAsync<Material>("Ice_Block_Material");
            await handle.Task;
            _iceMaterial = handle.Result;
            Addressables.Release(handle);

            handle = Addressables.LoadAssetAsync<Material>("Stone_Block_Material");
            await handle.Task;
            _stoneMaterial = handle.Result;
            Addressables.Release(handle);

            handle = Addressables.LoadAssetAsync<Material>("Obsidian_Block_Material");
            await handle.Task;
            _obsidianMaterial = handle.Result;
            Addressables.Release(handle);
        }
    }
}