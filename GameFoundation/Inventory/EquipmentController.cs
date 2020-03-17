using d4160.Core;
using UnityEngine;

namespace d4160.GameFoundation
{
    public class EquipmentController : MonoBehaviour
    {
        [SerializeField] protected EquipmentLocation[] _equipments;

        public GameObject GetEquipament(int index = 0)
        {
            if (_equipments.IsValidIndex(index))
            {
                return _equipments[index].Equipment;
            }

            return null;
        }

        public virtual void Equip(GameObject item, int index = 0)
        {
            if (_equipments.IsValidIndex(index))
            {
                _equipments[index].Equipment = item;
            }
        }

        public virtual void Unequip(int index = 0)
        {
            if (_equipments.IsValidIndex(index))
            {
                _equipments[index].Equipment = null;
            }
        }
    }

    [System.Serializable]
    public struct EquipmentLocation
    {
        public Transform location;
        public GameObject equipment;

        public GameObject Equipment
        {
            get => equipment;
            set
            {
                if (value == null && equipment)
                {
                    equipment.SetActive(false);
                }

                equipment = value;

                if (equipment && location)
                {
                    equipment.transform.SetParent(location, false);
                    equipment.SetActive(true);
                }
            }
        }
    }
}